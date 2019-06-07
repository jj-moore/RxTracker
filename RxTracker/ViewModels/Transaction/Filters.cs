using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Transaction
{
    public class Filters
    {
        public int DrugId { get; set; }
        public int DoctorId { get; set; }
        public int PharmacyId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? CostFrom { get; set; }
        public decimal? CostTo { get; set; }
        public bool IncludeInactive { get; set; }
        public bool IncludeBrandedAndGeneric { get; set; }
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
}
