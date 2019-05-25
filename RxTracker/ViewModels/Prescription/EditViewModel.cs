using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Prescription
{
    public class EditViewModel
    {
        public Models.Prescription Prescription { get; set; }
        public List<SelectHelper> Doctors { get; set; }
        public List<SelectHelper> Drugs { get; set; }

        public EditViewModel()
        {
            Doctors = new List<SelectHelper>();
            Drugs = new List<SelectHelper>();
        }
    }
}
