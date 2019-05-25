using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Hospital { get; set; }
        public string Address { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
