﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class WeekModel
    {
        public int Number { get; set; }
        public string Status { get; set; }
        public string Mark { get; set; }
        public ObservableCollection<DayModel> Days { get; set; }
    }
}