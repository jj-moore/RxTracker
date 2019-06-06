using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels.Pharmacy;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public PharmacyController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The method queries the database to get a list of pharmacies associated 
        /// with this user. From here the user can intiate CRUD operations on
        /// their pharmacies. It is called by the browser navigating to 
        /// https://[hostname]/Pharmacy. The list of pharmacies will appear on the left. 
        /// Selecting a specific pharmacy will display the details on the right, from 
        /// which the user can update, insert and delete a pharmacy.
        /// </summary>
        /// <returns>Html representing the web page</returns>
        public ActionResult Index()
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE FOR THE INFORMATION REQUIRED TO MAKE A LIST OF PHARMACIES
            var model = new ListViewModel
            {
                PharmacyList = _context.Pharmacy
                    .Where(p => p.User == user)
                    .Select(p => new PharmacyListItem
                    {
                        PharmacyId = p.PharmacyId,
                        Name = p.Name,
                        Address = p.Address
                    })
                    .OrderBy(p => p.Name)
                    .ToList()
            };

            // GENERATE THE HTML TO RETURN
            return View(model);
        }

        /// <summary>
        /// This method queries the database to find a specific pharmay based on the.
        /// id. The details are returned as html and displayed on the web page. The
        /// pharmacy must exist in the database and it must be associated with the user
        /// making this request. If the pharmacy is not found, it is assumed the user is 
        /// requesting a blank form for entering a new pharmacy.
        /// </summary>
        /// <param name="id">The PharmacyId for which details are being requested</param>
        /// <returns>html representing a partial web page showing the details of a
        /// pharmacy</returns>
        public ActionResult Details(int id)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE FOR THE PHARMACY USING THE PRIMARY KEY
            Pharmacy pharmacy = _context.Pharmacy.Find(id);

            // IF THE PHARMACY IS NOT ASSOCIATED WITH THE CURRENT USER, RETURN 401
            if (pharmacy != null && pharmacy.User != user)
            {
                return Unauthorized();
            }

            // IF THE PHARMACY WAS NOT FOUND, ASSUME USER WANTS A BLANK PHARMACY
            if (pharmacy == null)
            {
                pharmacy = new Pharmacy();
            }

            return PartialView("_PharmacyPartial", pharmacy);
        }

        /// <summary>
        /// This method will insert or update a pharmacy based on the parameters received.
        /// If the PharmacyId = 0, this method will create a pharmacy from the information
        /// received. If the PharmacyId != 0, this method will query the database to locate
        /// the pharmacy, confirm it is associated with the current user and update the  
        /// pharmacy with the information received.
        /// </summary>
        /// <param name="pharmacy">The object containing the updated pharmacy information</param>
        /// <returns>Redirects the user to the Pharmacy List with the information updated</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pharmacy pharmacy)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // IF THE PHARMACY ID = 0, THE USER IS ADDING A NEW PHARMACY.
            // ASSOCIATE THE CURRENT USER WITH THE PHARMACY AND ADD IT TO THE LIST OF UPDATES
            if (pharmacy.PharmacyId == 0)
            {
                pharmacy.User = user;
                _context.Pharmacy.Add(pharmacy);
            }
            else
            {
                // CALLS A FUNCTION TO ADD UPDATE THE PHARMACY
                EditPharmacy(pharmacy, user);
            }

            // EXCUTE THE INSERT OR UPDATE STATEMENT
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
        /// Method to handle the deleting of a Pharmacy. This method is called from 
        /// JavaScript and, after confirming the drug is associated with the
        /// current user, will execute the delete statement.
        /// </summary>
        /// <param name="id">The PharmacyId of the record to delete</param>
        /// <returns>Returns a 204 if the record is not found, 401 if this record
        /// is not associated with this user, 500 if there is an error while deleting
        /// or 200 if the record is successfully deleted</returns>
        [HttpPost]
        public ActionResult Delete([FromBody]int id)
        {
            // QUERY THE DATABASE FOR THE PHARMACY WITH THE PRIMARY KEY
            Pharmacy pharmacyToDelete = _context.Pharmacy.Find(id);

            // IF THE PHARMACY WAS NOT FOUND, RETURN 204
            if (pharmacyToDelete == null)
            {
                return NoContent();
            }

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // CONFIRM THE CURRENT USER MATCHES THE USER ASSIGNED TO THIS RECORD
            if (pharmacyToDelete.User != user)
            {
                return Unauthorized();
                //TODO: Handle the unauthorized reponse 
            }

            // FLAG THE PHARMACY FOR REMOVAL
            _context.Pharmacy.Remove(pharmacyToDelete);

            // EXECUTE THE DELETE STATEMENT
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
                // TODO: Handle the failure in JavaScript
            }

            return Ok();
        }

        /// <summary>
        /// Method to handled the updating of a pharmacy. After confirming the pharmacy is associated
        /// with the current user, the details are set to the passed in parameters and
        /// a boolean response is returned to the caller to indicate if the in memory version
        /// of the pharmacy was successfully edited. This method does not persist the changes.
        /// The caller is required to do that.
        /// </summary>
        /// <param name="pharmacy">The details of the pharmacy to be edited</param>
        /// <param name="user">The user associated with this request</param>
        /// <returns>True if the pharamcy was loaded into memory from the database and sucessfully edited,
        /// false otherwise
        /// </returns>
        private bool EditPharmacy(Pharmacy pharmacy, MyUser user)
        {
            // QUERY THE DATABASE FOR THE PHARMACY BY THE PRIMARY KEY
            Pharmacy pharmacyToEdit = _context.Pharmacy.Find(pharmacy.PharmacyId);

            // IF THE PHARMACY IS NOT FOUND, OR IS NOT ASSOCIATED WITH THE CURRENT USER RETURN FALSE
            if (pharmacyToEdit == null || pharmacyToEdit.User != user)
            {
                return false;
            }

            // CHANGE THE APPROPRIATE FIELDS AND FLAG THEM FOR UPDATES
            pharmacyToEdit.Name = pharmacy.Name;
            pharmacyToEdit.Address = pharmacy.Address;
            return true;
        }
    }
}