using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using System.Linq;

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

        // GET: Transaction
        public ActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
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
                        DrugName = t.Prescription.Drug.Name,
                        TradeName = t.Prescription.Drug.TradeName,
                        Pharmacy = t.Pharmacy.Name
                    })
                    .OrderBy(t => t.DateFilled)
                    .ToList()
            };
            return View(model);
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Transaction transaction = _context.Transaction
                .Include(t => t.Prescription)
                .FirstOrDefault(t => t.TransactionId == id);
            if (transaction != null && transaction.Prescription.User != user)
            {
                return Unauthorized();
            }
            if (transaction == null)
            {
                transaction = new Transaction();
            }

            var model = new ViewModels.Transaction.EditViewModel
            {
                Transaction = transaction,
                Prescription = _context.Prescription
                    .Include(p => p.Drug)
                    .Where(p => p.Drug.User == user)
                    .Select(p => new ViewModels.SelectHelper
                    {
                        Value = p.PrescriptionId,
                        Text = p.Drug.Name
                    })
                    .ToList(),
                Pharmacy = _context.Pharmacy
                    .Where(p => p.User == user)
                    .Select(p => new ViewModels.SelectHelper
                    {
                        Value = p.PharmacyId,
                        Text = p.Name
                    })
                    .ToList(),
            };

            return PartialView("_TransactionPartial", model);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (transaction.TransactionId == 0)
            {
                _context.Transaction.Add(transaction);
            }
            else
            {
                EditTransaction(transaction, user);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Transaction transactionToDelete = _context.Transaction.Find(id);
            if (transactionToDelete == null)
            {
                return NoContent();
            }

            _context.Transaction.Remove(transactionToDelete);
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

        private bool EditTransaction(Transaction transaction, MyUser user)
        {
            Transaction transactionToEdit = _context.Transaction
               .Include(t => t.Prescription)
                .FirstOrDefault(t => t.TransactionId == transaction.TransactionId);
            if (transactionToEdit == null || transactionToEdit.Pharmacy.User != user)
            {
                return false;
            }

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