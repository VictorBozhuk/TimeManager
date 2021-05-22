using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Models.StatisticModels;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticGlobalTasksViewModel : GlobalBaseViewModel
    {
        public override List<string> GlobalPeriods { get; set; } = new List<string>()
            { Periods.Week, Periods.Month, Periods.Quarter, Periods.Year, Periods.More, Periods.Future, Periods.Templates };

        public override string SelectedGlobalPeriod
        {
            get
            {
                return selectedGlobalPeriod;
            }
            set
            {
                selectedGlobalPeriod = value;
                SetGlobalTasks(value, false);

                var st = DateShortFrom;
                DateShortFrom = DateShortTo;
                DateShortTo = st;

                st = DateLongFrom;
                DateLongFrom = DateLongTo;
                DateLongTo = st;

                CalculateGlobalTasks(GlobalTasks);
            }
        }

        public StatisticGlobalTasksViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectedGlobalPeriod = GlobalPeriods.First();
        }

        private void CalculateGlobalTasks(List<GlobalTaskModel> globalTasks)
        {
            foreach (var item in globalTasks)
            {
                var sumHoursInProc = item.DailyTasks.Select(x => GetSpentTime(x).TotalHours).Sum().ToString();
                var times = item.DailyTasks.Select(x => GetSpentTime(x));
                int hours = 0;
                int minutes = 0;
                foreach (var item2 in times)
                {
                    hours += item2.Hours;
                    minutes += item2.Minutes;
                }

                hours += minutes / 60;
                item.Hours = hours.ToString();
                item.Minutes = (minutes % 60).ToString();
                item.TimeSpent = $"{hours}:{minutes % 60}";
                item.TimeSpentByProcents = sumHoursInProc;
            }

        }

        private TimeSpan GetSpentTime(DailyTaskModel task)
        {
            TimeSpan start = TimeSpan.Parse(task.Start);
            TimeSpan end = TimeSpan.Parse(task.End);
            var res = end - start;
            if (res.Hours < 0)
            {
                res = TimeSpan.Parse("23:59:59") + res;
                res = new TimeSpan(int.Parse(res.ToString("hh")), res.Minutes, res.Seconds + 1);
            }

            return res;
        }

        protected override bool CheckDates(DateTime deadLine, DateTime dateMin, DateTime dateLimit)
        {
            return deadLine < dateMin && deadLine > dateLimit;
        }

        protected override bool CheckDates(DateTime deadLine, DateTime dateMin)
        {
            return deadLine < dateMin;
        }
    }
}
