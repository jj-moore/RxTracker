using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using System;
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

        /// <summary>
        /// This is the landing page after a user authenticates. It queries the database
        /// the information required to display a summary of the last transaction for all 
        /// active prescriptions and total cost over the last month.
        /// </summary>
        /// <returns>A web page with a summary of active prescriptions</returns>
        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var groups = _context.Transaction
                 .Include(t => t.Pharmacy)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Doctor)
                 .Include(t => t.Prescription)
                     .ThenInclude(p => p.Drug)
                 .Where(t => t.Prescription.User == user && t.Prescription.Active)
                 .GroupBy(p => p.Prescription.DrugId)
                 .AsNoTracking();

            var priorMonthCost = _context.Transaction
                    .Include(t => t.Prescription)
                    .Where(t => t.Prescription.User == user)
                    .Where(t => DateTime.Now.AddMonths(-1).CompareTo(t.DateFilled) < 0)
                    .Select(t => t.Cost)
                    .Sum() ?? 0;

            DashboardViewModel model = new DashboardViewModel
            {
                PriorMonthCost = priorMonthCost.ToString("C")
            };

            foreach (var group in groups)
            {
                Transaction transaction = group
                    .OrderByDescending(t => t.DateFilled)
                    .FirstOrDefault();

                if (transaction != null)
                {
                    model.Dashboard.Add(new DashboardRecord
                    {
                        DrugDisplayName = transaction.Prescription.Drug.DisplayName,
                        DoctorName = transaction.Prescription.Doctor.Name,
                        PharmacyName = transaction.Pharmacy.Name,
                        LastFilled = transaction.DateFilled?.ToShortDateString(),
                        LastCost = transaction.Cost?.ToString("C")
                    });
                }
            }

            model.Dashboard = model.Dashboard.OrderBy(d => d.LastFilled).ToList();
            return View(model);
        }

        /// <summary>
        /// Called by JavaScript to update the cost for x number of months.
        /// </summary>
        /// <param name="months">The number of months to include in the cost calculation</param>
        /// <returns>A JSON formatted string of the cost</returns>
        public IActionResult GetCost(int months)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var priorMonthCost = _context.Transaction
                    .Where(t => t.Prescription.User == user)
                    .Where(t => DateTime.Now.AddMonths(months * -1).CompareTo(t.DateFilled) < 0)
                    .Select(t => t.Cost)
                    .Sum() ?? 0;

            return Json(new
            {
                cost = priorMonthCost.ToString("C")
            });
        }
    }
}