using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Transaction
{
    public class EditViewModel
    {
        public Models.Transaction Transaction { get; set; }
        public List<SelectHelper> Prescription { get; set; }
        public List<SelectHelper> Pharmacy { get; set; }

        public EditViewModel()
        {
            Prescription = new List<SelectHelper>();
            Pharmacy = new List<SelectHelper>();
        }
    }
}
