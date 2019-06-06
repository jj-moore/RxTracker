using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using RxTracker.ViewModels.Prescription;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public PrescriptionController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The method queries the database to get a list of prescriptions associated 
        /// with this user. From here the user can intiate CRUD operations on
        /// their prescriptions. It is called by the browser navigating to 
        /// https://[hostname]/Prescription. The list of prescriptions will appear on the left. 
        /// Selecting a specific prescriptions will display the details on the right, from 
        /// which the user can update, insert and delete a record.
        /// </summary>
        /// <returns>Html representing the web page</returns>
        public ActionResult Index()
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE FOR THE INFORMATION REQUIRED TO BUILD THE LIST
            ListViewModel model = new ListViewModel
            {
                PrescriptionList = _context.Prescription
                    .Where(p => p.Active && p.User == user)
                    .Include(p => p.Drug)
                    .Include(p => p.Doctor)
                    .Select(p => new PrescriptionListItem
                    {
                        PrescriptionId = p.PrescriptionId,
                        DrugDisplayName = p.Drug.DisplayName,
                        DoctorName = p.Doctor.Name
                    }).ToList()
            };

            // GENERATE THE HTML AND RETURN IT
            return View(model);
        }

        /// <summary>
        /// This method queries the database to find a specific prescription based on the.
        /// id. The details are returned as html and displayed on the web page. The
        /// prescription must exist in the database and it must be associated with the user
        /// making this request. If the prescription is not found, it is assumed the user is 
        /// requesting a blank form for entering a new record.
        /// </summary>
        /// <param name="id">The PrescriptionId for which details are being requested</param>
        /// <returns>html representing a partial web page showing the details of a
        /// prescription</returns>
        public ActionResult Details(int id)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE FOR THE PRESCRIPTION
            Prescription prescription = _context.Prescription.Find(id);

            // IF THE PRESCRIPTION IS NOT ASSOCIATED WITH THIS USER, RETURN 401
            if (prescription != null && prescription.User != user)
            {
                return Unauthorized();
            }

            // IF THE PRESCRIPTION IS NOT FOUND, RETURN A BLANK TEMPLATE
            if (prescription == null)
            {
                prescription = new Prescription();
            }

            // PUT TOGETHER ALL INFORMATION REQUIRED FOR THE PAGE
            // INCLUDE INFORMATION FOR SELECT LISTS
            EditViewModel model = new EditViewModel
            {
                Prescription = prescription,
                Doctors = _context.Doctor
                    .Where(d => d.User == user)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DoctorId,
                        Text = d.Name
                    })
               .ToList(),
                Drugs = _context.Drug
                    .Where(d => d.User == user)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                    })
                   .ToList(),
            };

            // INSERT BLANK VALUES FOR THE SELECT LISTS
            model.Doctors.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });
            model.Drugs.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });

            // GENERATE THE HTML AND RETURN
            return PartialView("_PrescriptionPartial", model);
        }

        /// <summary>
        /// This method will insert or update a prescription based on the parameters received.
        /// If the PrescriptionId = 0, this method will create a prescription from the information
        /// received. If the PrescriptionId != 0, this method will query the database to locate
        /// the prescription, confirm it is associated with the current user and update the  
        /// prescription with the information received.
        /// </summary>
        /// <param name="prescription">The object containing the updated prescription information</param>
        /// <returns>Redirects the user to the prescription list with the information updated</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prescription prescription)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // IF THE ID = 0, USER IS ATTEMPTING TO ADD A PRESCRIPTION
            // ASSOCIATE THE USER WITH THE PRESCRIPTION AND FLAG FOR INSERT
            if (prescription.PrescriptionId == 0)
            {
                prescription.User = user;
                _context.Prescription.Add(prescription);
            }
            else
            {
                // MAKE REQUESTED EDITS AND FLAG FOR UPDATE
                EditPrescription(prescription, user);
            }

            // EXECUTE THE INSERT OR UPDATE
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
        /// Method to handle the deleting of a prescription. This method is called from 
        /// JavaScript and, after confirming the drug is associated with the
        /// current user, will execute the delete statement.
        /// </summary>
        /// <param name="id">The PharmacyId of the record to delete</param>
        /// <returns>Returns a 204 if the record is not found, 401 if this record
        /// is not associated with this user, 500 if there is an error while deleting
        /// or 200 if the record is successfully deleted</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // QUERY THE DATABASE TO RETRIEVE THE PRESCRIPTION TO DELETE
            Prescription prescriptionToDelete = _context.Prescription.Find(id);

            // IF NOT FOUND, RETURN 204
            if (prescriptionToDelete == null)
            {
                return NoContent();
            }

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // CONFIRM THE CURRENT USER MATCHES THE USER ASSIGNED TO THIS RECORD
            if (prescriptionToDelete.User != user)
            {
                return Unauthorized();
                //TODO: Handle the unauthorized reponse 
            }

            // FLAG THE RECORD FOR REMOVAL
            _context.Prescription.Remove(prescriptionToDelete);

            // EXECUTE THE DELETE
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        /// <summary>
        /// Method to handled the updating of a prescription. After confirming the record is associated
        /// with the current user, the details are set to the passed in parameters and
        /// a boolean response is returned to the caller to indicate if the in memory version
        /// of the prescription was successfully edited. This method does not persist the changes.
        /// The caller is required to do that.
        /// </summary>
        /// <param name="prescription">The details of the prescription to be edited</param>
        /// <param name="user">The user associated with this request</param>
        /// <returns>True if the prescription was loaded into memory from the database and sucessfully edited,
        /// false otherwise
        /// </returns>
        private bool EditPrescription(Prescription prescription, MyUser user)
        {
            // QUERY THE DATABASE FOR THE PRESCRIPTION TO EDIT
            Prescription prescriptionToEdit = _context.Prescription.Find(prescription.PrescriptionId);

            // IF THE PRESCRIPTION IS NOT FOUND OR IS NOT ASSOCATED WITH THE CURRENT USER, RETURN FALSE.
            if (prescriptionToEdit == null || prescriptionToEdit.User != user)
            {
                return false;
            }

            // MAKE REQUESTED UPDATES AND FLAG FOR EXECUTION
            prescriptionToEdit.DoctorId = prescription.DoctorId;
            prescriptionToEdit.DrugId = prescription.DrugId;
            prescriptionToEdit.Active = prescription.Active;
            prescriptionToEdit.Form = prescription.Form;
            prescriptionToEdit.Dosage = prescription.Dosage;
            prescriptionToEdit.Regimen = prescription.Regimen;
            return true;
        }
    }
}