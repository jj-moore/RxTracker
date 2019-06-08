using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using RxTracker.ViewModels.Statistics;
using RxTracker.ExtensionMethods;

namespace RxTracker.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;
        private decimal _totalCost;

        public StatisticsController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Filters filters = new Filters
            {
                DateFrom = DateTime.Now.AddMonths(-3),
                SortBy = "DateFilled",
                SortDescending = true
            };

            Statistics model = new Statistics
            {
                StatisticsList = GetStatistics(filters),
                DrugList = _context.Drug
                    .OrderBy(d => d.DisplayName)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = d.DisplayName
                    })
                    .ToList(),
                DoctorList = _context.Doctor
                    .OrderBy(d => d.Name)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DoctorId,
                        Text = d.Name
                    })
                    .ToList(),
                PharmacyList = _context.Pharmacy
                    .OrderBy(d => d.Name)
                    .Select(d => new SelectHelper
                    {
                        Value = d.PharmacyId,
                        Text = d.Name
                    })
                    .ToList(),
                SortList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "DrugName", Text = "Medication" },
                    new SelectListItem { Value = "DoctorName", Text = "Doctor" },
                    new SelectListItem { Value = "PharmacyName", Text = "Pharmacy" },
                    new SelectListItem { Value = "DateFilled", Text = "Date" },
                    new SelectListItem { Value = "Cost", Text = "Cost" },
                },
                DefaultSort = "DateFilled"
            };

            model.TotalCost = _totalCost.ToString("C");

            var blank = new SelectHelper
            {
                Value = 0,
                Text = ""
            };
            model.DrugList.Insert(0, blank);
            model.DoctorList.Insert(0, blank);
            model.PharmacyList.Insert(0, blank);

            return View(model);
        }

        [HttpPost]
        public IActionResult GetStatisticsJson([FromBody]Filters filters)
        {
            List<Statistic> model = GetStatistics(filters);
            return PartialView("_StatisticsPartial", model);
        }


        private List<Statistic> GetStatistics(Filters filters)
        {
            IQueryable<Transaction> statisticsQueryable = _context.Transaction
               .Include(t => t.Pharmacy)
               .Include(t => t.Prescription)
                   .ThenInclude(p => p.Drug)
               .Include(t => t.Prescription)
                   .ThenInclude(p => p.Doctor);

            statisticsQueryable = statisticsQueryable.FilterStatistics(_context, filters);
            statisticsQueryable = statisticsQueryable.SortStatistics(filters.SortBy, filters.SortDescending);

            _totalCost = statisticsQueryable
                .AsNoTracking()
                .Select(s => s.Cost)
                .Sum() ?? 0;

            List<Statistic> statistics = statisticsQueryable
               .Select(t => new Statistic
               {
                   TransactionId = t.TransactionId,
                   DrugName = t.Prescription.Drug.DisplayName,
                   Dosage = t.Prescription.Dosage,
                   DoctorName = t.Prescription.Doctor.Name,
                   PharmacyName = t.Pharmacy.Name,
                   DateFilled = t.DateFilled.Value.ToShortDateString(),
                   Cost = t.Cost.Value.ToString("C")
               })
               .AsNoTracking()
               .ToList();

            return statistics;
        }
    }
}