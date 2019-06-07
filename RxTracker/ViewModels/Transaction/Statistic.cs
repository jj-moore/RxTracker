using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Transaction
{
    public class Statistic
    {
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string DoctorName { get; set; }
        public string PharmacyName { get; set; }
        public string DateFilled { get; set; }
        public string Cost { get; set; }
    }
}
