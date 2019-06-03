using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Drug
    {
        public int DrugId { get; set; }
        [Required]
        [Display(Name = "Medication Name")]
        public string Name { get; set; }
        [Display(Name = "Trade Name")]
        public string TradeName { get; set; }
        public string Manufacturer { get; set; }
        [Display(Name = "Generic For")]
        public int? GenericForId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual MyUser User { get; set; }
        [NotMapped]
        public string DisplayName
        {
            get
            {
                return string.IsNullOrEmpty(TradeName) ? Name : $"{TradeName} ({Name})";
            }
        }


        public Drug GenericFor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
