using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [DisplayName("Medication")]
        [Required]
        public int PrescriptionId { get; set; }
        [DisplayName("Pharmacy")]
        [Required]
        public int PharmacyId { get; set; }
        [DisplayName("Date")]
        [Required]
        public DateTime? DateFilled { get; set; } = DateTime.Now;
        public decimal? Cost { get; set; }
        [DisplayName("Insurance Used")]
        public string InsuranceUsed { get; set; }
        [DisplayName("Discounts Used")]
        public string DiscountUsed { get; set; }

        public Prescription Prescription { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
