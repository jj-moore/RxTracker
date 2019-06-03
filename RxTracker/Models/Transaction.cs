using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [DisplayName("Medication")]
        public int PrescriptionId { get; set; }
        [DisplayName("Pharmacy")]
        public int PharmacyId { get; set; }
        [DisplayName("Date")]
        public DateTime? DateFilled { get; set; }
        public decimal? Cost { get; set; }
        [DisplayName("Insurance Used")]
        public string InsuranceUsed { get; set; }
        [DisplayName("Discounts Used")]
        public string DiscountUsed { get; set; }

        public Prescription Prescription { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
