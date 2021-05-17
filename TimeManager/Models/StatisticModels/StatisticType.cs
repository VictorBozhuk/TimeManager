using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models.StatisticModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticType
    {
        public string Title { get; set; }
        public string TimeSpent { get; set; }
        public string TimeSpentByProcents { get; set; }

        public StatisticType(string title, string timeSpant, string timeSpentByProcents)
        {
            Title = title;
            TimeSpent = timeSpant;
            TimeSpentByProcents = timeSpentByProcents;
        }
    }
}
