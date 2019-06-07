using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Transaction
{
    public class Statistics
    {
        public List<Statistic> StatisticsList { get; set; }
        public List<SelectHelper> DrugList { get; set; }
        public List<SelectHelper> DoctorList { get; set; }
        public List<SelectHelper> PharmacyList { get; set; }
    }
}
