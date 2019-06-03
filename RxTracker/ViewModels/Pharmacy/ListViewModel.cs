using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Pharmacy
{
    public class PharmacyListItem
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class ListViewModel
    {
        public List<PharmacyListItem> PharmacyList { get; set; }
    }
}
