using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Transaction
{
    public class TransactionListItem
    {
        public int TransactionId { get; set; }
        public string DrugDisplayName { get; set; }
        public string Pharmacy { get; set; }
        public string DateFilled { get; set; }
    }

    public class ListViewModel
    {
        public List<TransactionListItem> TransactionList { get; set; }
    }
}
