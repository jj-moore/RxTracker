using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels
{
    public class DashboardRecord
    {
        public string DrugName { get; set; }
        public string TradeName { get; set; }
        public string DoctorName { get; set; }
        public string PharmacyName { get; set; }
        public string LastFilled { get; set; }
        public string LastCost { get; set; }
        public string DrugDisplayName
        {
            get
            {
                return string.IsNullOrEmpty(TradeName) ?
                    DrugName :
                    $"{TradeName} ({DrugName})";
            }
        }
    }

    public class DashboardViewModel
    { 
        public List<DashboardRecord> Dashboard { get; set; }

        public DashboardViewModel()
        {
            Dashboard = new List<DashboardRecord>();
        }
    }
}
