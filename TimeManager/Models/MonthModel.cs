using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class MonthModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Mark { get; set; }
        public ObservableCollection<WeekModel> Weeks { get; set; }
        public ObservableCollection<DayModel> Days { get; set; }

    }
}
