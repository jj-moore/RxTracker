using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace RxTracker.ViewModels.Statistics
{
    public class Statistics
    {
        public List<Statistic> StatisticsList { get; set; }
        public List<SelectHelper> DrugList { get; set; }
        public List<SelectHelper> DoctorList { get; set; }
        public List<SelectHelper> PharmacyList { get; set; }
        public List<SelectListItem> SortList { get; set; }
        public string DefaultSort { get; set; }
        public string TotalCost { get; set; }
    }
}
