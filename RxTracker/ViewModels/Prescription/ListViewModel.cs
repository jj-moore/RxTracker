using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Prescription
{
    public class PrescriptionListItem
    {
        public int PrescriptionId { get; set; }
        public string DrugDisplayName { get; set; }
        public string DoctorName { get; set; }
    }

    public class ListViewModel
    {
        public List<PrescriptionListItem> PrescriptionList { get; set; }
    }
}
