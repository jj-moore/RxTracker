using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Drug
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string Manufacturer { get; set; }
        public int? GenericForId { get; set; }
        public virtual MyUser User { get; set; }


        public Drug GenericFor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
