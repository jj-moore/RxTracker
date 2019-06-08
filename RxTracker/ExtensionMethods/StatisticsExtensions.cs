using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ExtensionMethods
{
    public static class StatisticsExtensions
    {
        public static IQueryable<Transaction> FilterStatistics(
            this IQueryable<Transaction> statisticsQueryable, 
            ApplicationDbContext context, 
            Filters filters)
        {
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
                    int? genericForId = context.Drug.Find(filters.DrugId).GenericForId;
                    if (genericForId.HasValue)
                    {
                        drugIdList.Add(genericForId.Value);
                    }
                    drugIdList.AddRange(context.Drug
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

            return statisticsQueryable;
        }

        public static IQueryable<Transaction> SortStatistics(this IQueryable<Transaction> statisticsQueryable, string sortBy, bool sortDescending)
        {
            switch (sortBy)
            {
                case "DrugName":
                    if (sortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Prescription.Drug.DisplayName);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Prescription.Drug.DisplayName);
                    }
                    break;
                case "DoctorName":
                    if (sortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Prescription.Doctor.Name);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Prescription.Doctor.Name);
                    }
                    break;
                case "PharmacyName":
                    if (sortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Pharmacy.Name);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Pharmacy.Name);
                    }
                    break;
                case "DateFilled":
                    if (sortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.DateFilled);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.DateFilled);
                    }
                    break;
                case "Cost":
                    if (sortDescending)
                    {
                        statisticsQueryable = statisticsQueryable.OrderByDescending(t => t.Cost);
                    }
                    else
                    {
                        statisticsQueryable = statisticsQueryable.OrderBy(t => t.Cost);
                    }
                    break;
            }

            return statisticsQueryable;
        }
    }
}
