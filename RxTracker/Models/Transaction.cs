using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int PrescriptionId { get; set; }
        public int PharmacyId { get; set; }
        public DateTime DateFilled { get; set; }
        public decimal Cost { get; set; }
        public string InsuranceUsed { get; set; }
        public string DiscountUsed { get; set; }

        public Prescription Prescription { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
