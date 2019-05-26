using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.ViewModels;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var groups = _context.Transaction
                 .Include(t => t.Pharmacy)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Patient)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Doctor)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Drug)
                 .GroupBy(p => p.Prescription.DrugId);

            DashboardViewModel model = new DashboardViewModel();

            foreach (var group in groups)
            {
                RxTracker.Models.Transaction transaction = group
                    .OrderByDescending(t => t.DateFilled)
                    .FirstOrDefault();

                model.Dashboard.Add(new DashboardRecord
                {
                    DrugName = string.IsNullOrEmpty(transaction.Prescription.Drug.TradeName) ?
                        transaction.Prescription.Drug.Name : transaction.Prescription.Drug.TradeName,
                    DoctorName = transaction.Prescription.Doctor.Name,
                    PharmacyName = transaction.Pharmacy.Name,
                    LastFilled = transaction.DateFilled.ToShortDateString(),
                    LastCost = transaction.Cost.ToString("C")
                });
            }
            return View(model);
        }
    }
}