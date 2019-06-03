using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        [Display(Name = "Medication")]
        public int DrugId { get; set; }
        public bool Active { get; set; } = true;
        public string Form { get; set; }
        public string Dosage { get; set; }
        public string Regimen { get; set; }
        public string UserId { get; set; }

        public Doctor Doctor { get; set; }
        public Drug Drug { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public virtual MyUser User { get; set; }
    }
}
