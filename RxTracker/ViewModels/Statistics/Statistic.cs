using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Statistics
{
    public class Statistic
    {
        public int TransactionId { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string DoctorName { get; set; }
        public string PharmacyName { get; set; }
        public string DateFilled { get; set; }
        public string Cost { get; set; }
    }
}
