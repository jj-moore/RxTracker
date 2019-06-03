using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var groups = _context.Transaction
                 .Include(t => t.Pharmacy)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Doctor)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Drug)
                 .Where(t => t.Prescription.User == user)
                 .GroupBy(p => p.Prescription.DrugId)
                 .AsNoTracking();

            DashboardViewModel model = new DashboardViewModel();

            foreach (var group in groups)
            {
                Transaction transaction = group
                    .OrderByDescending(t => t.DateFilled)
                    .FirstOrDefault();

                if (transaction != null)
                {
                    model.Dashboard.Add(new DashboardRecord
                    {
                        TradeName = transaction.Prescription.Drug.TradeName,
                        DrugName = transaction.Prescription.Drug.Name,
                        DoctorName = transaction.Prescription.Doctor.Name,
                        PharmacyName = transaction.Pharmacy.Name,
                        LastFilled = transaction.DateFilled?.ToShortDateString(),
                        LastCost = transaction.Cost?.ToString("C")
                    });
                }
            }
            return View(model);
        }
    }
}