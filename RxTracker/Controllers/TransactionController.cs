using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using System.Linq;

/* Filter Transactions By: Date, Medication, Doctor, Pharmacy
All Current Prescriptions
Total Cost By: Date, Medication, Doctor, Pharmacy */

namespace RxTracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public TransactionController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// The method queries the database to get a list of transactions associated 
        /// with this user. From here the user can intiate CRUD operations on
        /// their transactions. It is called by the browser navigating to 
        /// https://[hostname]/Transaction. The list of transactions will appear on the left. 
        /// Selecting a specific transactions will display the details on the right, from 
        /// which the user can update, insert and delete a record.
        /// </summary>
        /// <returns>Html representing the web page</returns>
        public ActionResult Index()
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE FOR THE INFORMATION REQUIRED TO POPULATE THE LIST
            var model = new ViewModels.Transaction.ListViewModel
            {
                TransactionList = _context.Transaction
                    .Where(t => t.Prescription.User == user)
                    .Include(t => t.Pharmacy)
                    .Include(t => t.Prescription)
                        .ThenInclude(p => p.Drug)
                    .Select(t => new ViewModels.Transaction.TransactionListItem
                    {
                        TransactionId = t.TransactionId,
                        DateFilled = t.DateFilled.HasValue ? t.DateFilled.Value.ToShortDateString() : null,
                        DrugDisplayName = t.Prescription.Drug.DisplayName,
                        Pharmacy = t.Pharmacy.Name
                    })
                    .OrderBy(t => t.DateFilled)
                    .ToList()
            };

            // GENERATE THE HTML AND RETURN TO THE BROWSER
            return View(model);
        }

        /// <summary>
        /// This method queries the database to find a specific transaction based on the.
        /// id. The details are returned as html and displayed on the web page. The
        /// transaction must exist in the database and it must be associated with the user
        /// making this request. If the transaction is not found, it is assumed the user is 
        /// requesting a blank form for entering a new record.
        /// </summary>
        /// <param name="id">The TransactionId for which details are being requested</param>
        /// <returns>html representing a partial web page showing the details of a
        /// transaction</returns>
        public ActionResult Details(int id)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // QUERY THE DATABASE TO FIND THE TRANSACTION AND THE RELATED PRESCRIPTION
            Transaction transaction = _context.Transaction
                .Include(t => t.Prescription)
                .FirstOrDefault(t => t.TransactionId == id);

            // IF THE MEDICATION ON THIS TRANSACTION IS NOT RELATED TO THE
            // CURRENT USER, RETURN 401
            if (transaction != null && transaction.Prescription.User != user)
            {
                return Unauthorized();
            }

            // IF THE TRANSACTION WAS NOT FOUND, RETURN A BLANK TEMPLATE
            if (transaction == null)
            {
                transaction = new Transaction();
            }

            // PREPARE THE DATA FOR THE VIEW WITH ASSOCIATED DATA FOR SELECT LISTS
            var model = new ViewModels.Transaction.EditViewModel
            {
                Transaction = transaction,
                Prescription = _context.Prescription
                    .Include(p => p.Drug)
                    .Where(p => p.User == user)
                    .Select(p => new SelectHelper
                    {
                        Value = p.PrescriptionId,
                        Text = p.Drug.DisplayName
                    })
                    .ToList(),
                Pharmacy = _context.Pharmacy
                    .Where(p => p.User == user)
                    .Select(p => new SelectHelper
                    {
                        Value = p.PharmacyId,
                        Text = p.Name
                    })
                    .ToList(),
            };

            // INSERT BLANK RECORDS INTO THE SELECT LIST
            model.Prescription.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });
            model.Pharmacy.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });

            // GENERATE THE REQUIRED HTML AND RETURN
            return PartialView("_TransactionPartial", model);
        }

        /// <summary>
        /// This method will insert or update a transaction based on the parameters received.
        /// If the TransactionId = 0, this method will create a prescription from the information
        /// received. If the TransactionId != 0, this method will query the database to locate
        /// the transaction, confirm it is associated with the current user and update the  
        /// transaction with the information received.
        /// </summary>
        /// <param name="prescription">The object containing the updated transaction information</param>
        /// <returns>Redirects the user to the transaction list with the information updated</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // IF THE TRANSACTION ID = 0, FLAG THE TRANSACTION FOR INSERTION
            if (transaction.TransactionId == 0)
            {
                _context.Transaction.Add(transaction);
            }
            // OTHERWISE EDIT THE REQUESTED DETAILS AND FLAG FOR UPDATE
            else
            {
                EditTransaction(transaction, user);
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
        /// Method to handle the deleting of a transaction. This method is called from 
        /// JavaScript and, after confirming the drug is associated with the
        /// current user, will execute the delete statement.
        /// </summary>
        /// <param name="id">The TransactionId of the record to delete</param>
        /// <returns>Returns a 204 if the record is not found, 401 if this record
        /// is not associated with this user, 500 if there is an error while deleting
        /// or 200 if the record is successfully deleted</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // QUERY THE DATABASE TO FIND THE TRANSACTION AND THE RELATED PRESCRIPTION
            Transaction transactionToDelete = _context.Transaction
                .Include(t => t.Prescription)
                .FirstOrDefault(t => t.TransactionId == id);

            // IF THE TRANSACTION WAS NOT FOUND, RETURN 204
            if (transactionToDelete == null)
            {
                return NoContent();
            }

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // CONFIRM THE CURRENT USER MATCHES THE USER ASSIGNED TO THIS RECORD
            if (transactionToDelete.Prescription.User != user)
            {
                return Unauthorized();
                //TODO: Handle the unauthorized reponse 
            }

            // FLAG THE TRANSACTION FOR REMOVAL
            _context.Transaction.Remove(transactionToDelete);

            // EXECUTE THE DELETE STATEMENT
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
        /// Method to handled the updating of a transaction. After confirming the record is
        /// associated with the current user, the details are set to the passed in parameters 
        /// and a boolean response is returned to the caller to indicate if the in memory version
        /// of the transaction was successfully edited. This method does not persist the changes.
        /// The caller is required to do that.
        /// </summary>
        /// <param name="transaction">The details of the transaction to be edited</param>
        /// <param name="user">The user associated with this request</param>
        /// <returns>True if the transaction was loaded into memory from the database and sucessfully edited,
        /// false otherwise
        /// </returns>
        private bool EditTransaction(Transaction transaction, MyUser user)
        {
            // QUERY THE DATABASE TO FIND THE TRANSACTION AND THE RELATED PRESCRIPTION
            Transaction transactionToEdit = _context.Transaction
               .Include(t => t.Prescription)
                .FirstOrDefault(t => t.TransactionId == transaction.TransactionId);

            // IF THE TRANSACTION IS NOT FOUND OR IS NOT ASSOCIATED WITH THIS USER RETURN FALSE
            if (transactionToEdit == null || transactionToEdit.Pharmacy.User != user)
            {
                return false;
            }

            // MAKE THE REQUESTED CHANGES AND FLAG FOR UPDATE
            transactionToEdit.PrescriptionId = transaction.PrescriptionId;
            transactionToEdit.PharmacyId = transaction.PharmacyId;
            transactionToEdit.DateFilled = transaction.DateFilled;
            transactionToEdit.Cost = transaction.Cost;
            transactionToEdit.InsuranceUsed = transaction.InsuranceUsed;
            transactionToEdit.DiscountUsed = transaction.DiscountUsed;
            return true;
        }
    }
}
