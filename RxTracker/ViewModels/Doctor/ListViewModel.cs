using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Doctor
{
    public class DoctorListItem
    {
        public int DoctorId { get; set; }
        public string Hospital { get; set; }
        public string Name { get; set; }
    }
    public class ListViewModel
    {
        public List<DoctorListItem> DoctorList { get; set; }
    }
}
