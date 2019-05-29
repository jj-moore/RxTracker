﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxTracker.ViewModels.Drug
{
    public class DrugListItem
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
    }

    public class ListViewModel
    {
        public List<DrugListItem> DrugList { get; set; }
    }
}
