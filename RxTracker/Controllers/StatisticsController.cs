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

namespace RxTracker.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

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
                DefaultSort = "DateFilled",
                DefaultDateFrom = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd")
            };

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

            if (filters != null)
            {
                if (!filters.IncludeInactive)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.Prescription.Active);
                }

                if (filters.DrugId != 0 && !filters.IncludeBrandedAndGeneric)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.Prescription.DrugId == filters.DrugId);
                }
                if (filters.DrugId != 0 && filters.IncludeBrandedAndGeneric)
                {
                    List<int> drugIdList = new List<int> { filters.DrugId };
                    int? genericForId = _context.Drug.Find(filters.DrugId).GenericForId;
                    if (genericForId.HasValue)
                    {
                        drugIdList.Add(genericForId.Value);
                    }
                    drugIdList.AddRange(_context.Drug
                        .Where(d => d.GenericForId.HasValue && d.GenericForId == filters.DrugId)
                        .Select(d => d.DrugId));

                    statisticsQueryable = statisticsQueryable
                        .Where(t => drugIdList.Contains(t.Prescription.DrugId));
                }
                if (filters.DoctorId != 0)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.Prescription.DoctorId == filters.DoctorId);
                }
                if (filters.PharmacyId != 0)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.PharmacyId == filters.PharmacyId);
                }
                if (filters.DateFrom.HasValue)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.DateFilled.Value.CompareTo(filters.DateFrom.Value) > 0);
                }
                if (filters.DateTo.HasValue)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.DateFilled.Value.CompareTo(filters.DateTo.Value) < 0);
                }
                if (filters.CostFrom.HasValue)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.Cost.Value > filters.CostFrom.Value);
                }
                if (filters.CostTo.HasValue)
                {
                    statisticsQueryable = statisticsQueryable.Where(t => t.Cost.Value < filters.CostTo.Value);
                }
            }

            switch (filters.SortBy)
            {
                case "DrugName":
                    if (filters.SortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Prescription.Drug.DisplayName);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Prescription.Drug.DisplayName);
                    }
                    break;
                case "DoctorName":
                    if (filters.SortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Prescription.Doctor.Name);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Prescription.Doctor.Name);
                    }
                    break;
                case "PharmacyName":
                    if (filters.SortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Pharmacy.Name);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Pharmacy.Name);
                    }
                    break;
                case "DateFilled":
                    if (filters.SortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.DateFilled);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.DateFilled);
                    }
                    break;
                case "Cost":
                    if (filters.SortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Cost);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Cost);
                    }
                    break;
            }

            List<Statistic> statistics = statisticsQueryable
               .Select(t => new Statistic
               {
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