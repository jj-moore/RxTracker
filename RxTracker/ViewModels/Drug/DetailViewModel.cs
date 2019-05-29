using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Drug
{
    public class DetailViewModel
    {
        public Models.Drug Drug { get; set; }
        public List<SelectHelper> GenericFor { get; set; }

        public DetailViewModel()
        {
            GenericFor = new List<SelectHelper>();
        }
    }
}
