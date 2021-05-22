using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeManager.Models.StatisticModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticType
    {
        public string Title { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string TimeSpent { get; set; }
        public string TimeSpentByProcents { get; set; }
        public Brush RowColor { get; set; }

        public StatisticType(string title, string hours, string minutes, string timeSpant, string timeSpentByProcents, int index = 1)
        {
            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
            Title = title;
            Hours = hours;
            Minutes = minutes;
            TimeSpent = timeSpant;
            TimeSpentByProcents = timeSpentByProcents;
        }
    }
}
