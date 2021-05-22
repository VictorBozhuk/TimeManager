using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeManager.Models.Abstracts;

namespace TimeManager.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DayModel
    {
        public Guid Id { get; set; }
        private DateTime date;
        public Brush RowColor { get; set; }

        public DayModel(DateTime date)
        {
            Date = date;
        }
        public DayModel(Day day, int index = 1)
        {
            Id = day.Id;
            Date = day.Date;
            var plans = day.DailyTasks.Where(x => x.IsPlan).ToList();
            var tasks = day.DailyTasks.Where(x => !x.IsPlan).ToList();
            DailyPlans = plans.Select(x => new DailyTaskModel(x, plans.IndexOf(x))).ToList();
            DailyTasks = tasks.Select(x => new DailyTaskModel(x, tasks.IndexOf(x))).ToList();
            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
        }
        public string DateShortString { get; set; }
        public string DateLongString { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime Date
        { 
            get { return date; }
            set
            {
                date = value;
                DateShortString = value.ToShortDateString();
                DateLongString = value.ToLongDateString();
                DayOfWeek = DayParser.TranslateDayOfWeek(value.DayOfWeek.ToString());
            }
        }
        public List<DailyTaskModel> DailyPlans { get; set; }
        public List<DailyTaskModel> DailyTasks { get; set; }

        public override string ToString()
        {
            return DateShortString;
        }
    }
}
