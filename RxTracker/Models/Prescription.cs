using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DrugId { get; set; }
        public bool Active { get; set; } = true;
        public string Form { get; set; }
        public string Dosage { get; set; }
        public string Regimen { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Drug Drug { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
