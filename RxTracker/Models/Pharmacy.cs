using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.Models
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public virtual MyUser User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
