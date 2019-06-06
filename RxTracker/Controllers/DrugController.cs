using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using RxTracker.ViewModels.Drug;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class DrugController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DrugController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The method queries the database to get a list of drugs associated 
        /// with this user. From here the user can intiate CRUD operations on
        /// their medications. It is called by the browser navigating to 
        /// https:// hostname /Drug. The list of drugs will appear on the left. 
        /// Selecting a specific drug will display the details on the right, from 
        /// which the user can update, insert and delete a drug.
        /// </summary>
        /// <returns>Html representing the web page</returns>
        public ActionResult Index()
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE TO GET INFORMATION NEEDED FOR THE SUMMARY LIST
            ListViewModel model = new ListViewModel
            {
                DrugList = _context.Drug
                    .Where(d => d.User == user)
                    .Select(d => new DrugListItem
                    {
                        DrugId = d.DrugId,
                        DisplayName = d.DisplayName
                    })
                    .OrderBy(d => d.DisplayName)
                    .ToList()
            };

            // RETURN THE VIEW POPULATED WITH INFORMATION FROM THE DATABASE
            return View(model);
        }

        /// <summary>
        /// This method queries the database to find a specific drug based on the id.
        /// The details are returned as html and displayed on the web page. The drug
        /// must exist in the database and it must be associated with the user making
        /// this request. If the drug is not found, it is assumed the user is 
        /// requesting a blank form for entering a new drug.
        /// </summary>
        /// <param name="id">The DoctorId for which details are being requested</param>
        /// <returns>html representing a partial web page showing the details of a
        /// drug</returns>
        public ActionResult Details(int id)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // FIND THE DRUG BY IT'S PRIMARY KEY
            Drug drug = _context.Drug.Find(id);

            // IF THE DRUG IS FOUND, BUT IS NOT ASSOCIATED WITH THIS USER, RETURN 401
            if (drug != null && drug.User != user)
            {
                return Unauthorized();
            }

            // IF THE DRUG IS NOT FOUND, RETURN A BLANK TEMPLATE FOR ENTERING A NEW DRUG
            if (drug == null)
            {
                drug = new Drug();
            }

            // IF THE DRUG IS FOUND, CREATE THE MODEL REQUIRED TO DISPLAY THE PAGE
            // THIS INCLUDES A LIST FOR THE 'GENERIC FOR' SELECT LIST.
            DetailViewModel model = new DetailViewModel
            {
                Drug = drug,
                GenericFor = _context.Drug
                    .Where(d => d.User == user && !string.IsNullOrEmpty(d.TradeName))
                    .Where(d => d.DrugId != drug.DrugId)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = d.DisplayName
                    })
                    .OrderBy(d => d.Text)
                    .ToList()
            };

            // INSERT A BLANK RECORD FOR THE SELECT LIST
            model.GenericFor.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });

            // GENERATE AND RETURN THE HTML
            return PartialView("_DrugPartial", model);
        }

        /// <summary>
        /// This method will insert or update a drug based on the parameters received.
        /// If the DrugId = 0, this method will create a drug from the information received.
        /// If the DrugId != 0, this method will query the database to locate the drug,
        /// confirm it is associated with the current user and update the drug with the 
        /// information received.
        /// </summary>
        /// <param name="drug">The object containing the updated drug information</param>
        /// <returns>Redirects the user to the DrugList with the information updated</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drug drug)
        {
            // IF THERE IS NO GENERIC, THE VALUE MUST BE SET TO NULL FOR THE DATABASE
            drug.GenericForId = drug.GenericForId == 0 ? null : drug.GenericForId;

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // IF THE DrugId = 0, THEN WE ARE INSERTING A DRUG. THE CURRENT USERS ID
            // IS SAVED WITH THE DRUG.
            if (drug.DrugId == 0)
            {
                drug.User = user;
                _context.Drug.Add(drug);
            }
            // IF THE DrugId != 0 THEN WE ARE EDITING A DRUG. A SEPERATE METHOD
            // IS CALLED TO HANDLE THE EDIT AND RETURNS HERE TO UPDATE THE DATABASE
            else
            {
                EditDrug(drug, user);
            }

            // EXECUTE THE QUERY TO INSERT OR UPDATE THE DATABASE
            // ON ERROR REDIRECT THE USER TO THE ERROR PAGE
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Method to handle the deleting of a Drug. This method is called from 
        /// JavaScript and, after confirming the drug is associated with the
        /// current user, will execute the delete statement.
        /// </summary>
        /// <param name="id">The DrugId of the drug to delete</param>
        /// <returns>Returns a 204 if the drug is not found, 401 if this drug
        /// is not associated with this user, 500 if there is an error while deleting
        /// or 200 if the drug is successfully deleted</returns>
        [HttpPost]
        public ActionResult Delete([FromBody]int id)
        {
            // QUERY THE DATABASE TO LOCATE THE DRUG TO DELETE
            Drug drugToDelete = _context.Drug.Find(id);

            // IF THE DRUG COULD NOT BE FOUND, RETURN 204
            if (drugToDelete == null)
            {
                return NoContent();
            }

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // CONFIRM THE CURRENT USER MATCHES THE USER ASSIGNED TO THIS DOCTOR
            if (drugToDelete.User != user)
            {
                return Unauthorized();
                //TODO: Handle the unauthorized reponse 
            }

            // REMOVE THE DRUG FROM THE DATABASE
            _context.Drug.Remove(drugToDelete);

            // EXECUTE THE QUERY
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            // RETURN 200 IF SUCCESSFULLY DELETED
            return Ok();
        }

        /// <summary>
        /// Method to handled the updating of a drug. After confirming the drug is associated
        /// with the current user, the drug details are set to the passed in parameters and
        /// a boolean response is returned to the caller to indicate if the in memory
        /// version of the drug was successfully edited. This method does not persist the changes.
        /// The caller is required to do that.
        /// </summary>
        /// <param name="drug">The details of the drug to be edited</param>
        /// <param name="user">The user associated with this request</param>
        /// <returns>True if the drug was loaded into memory from the database and sucessfully edited,
        /// false otherwise
        /// </returns>
        private bool EditDrug(Drug drug, MyUser user)
        {
            // LOAD THE DRUG FROM THE DATABASE INTO MEMORY
            Drug drugToEdit = _context.Drug.Find(drug.DrugId);

            // CONFIRM THE DRUG WAS FOUND AND IS ASSOCIATED WITH THE CURRENT USER
            if (drugToEdit == null || drugToEdit.User != user)
            {
                return false;
            }

            // SET THE DRUG DETAILS TO THOSE IN THE PARAMETER
            drugToEdit.Name = drug.Name;
            drugToEdit.TradeName = drug.TradeName;
            drugToEdit.Manufacturer = drug.Manufacturer;
            drugToEdit.GenericForId = drug.GenericForId;
            return true;
        }
    }
}