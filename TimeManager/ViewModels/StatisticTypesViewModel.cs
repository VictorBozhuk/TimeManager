using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Models.StatisticModels;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticTypesViewModel : BaseViewModel
    {
        protected string selectedTypePeriod;

        public List<string> TypePeriods { get; set; } = new List<string>()
            { Periods.Week, Periods.Month, Periods.Quarter, Periods.Year, Periods.More};

        public string SelectedTypePeriod
        {
            get
            {
                return selectedTypePeriod;
            }
            set
            {
                selectedTypePeriod = value;
                SetTypes(value);
            }
        }

        public ObservableCollection<StatisticType> Types { get; set; } = new ObservableCollection<StatisticType>();

        public string DateShortFrom { get; set; }
        public string DateLongFrom { get; set; }
        public string DateShortTo { get; set; }
        public string DateLongTo { get; set; }

        public StatisticTypesViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectedTypePeriod = TypePeriods.First();
        }

        private void CalculateTypes(IEnumerable<IGrouping<string, DailyTaskModel>> typesWithTasks)
        {
            var newTypes = new List<StatisticType>();

            foreach (var item in typesWithTasks)
            {
                var sumHoursInProc = Math.Round(item.Select(x => GetSpentTime(x).TotalHours).Sum(), 2).ToString();
                var times = item.Select(x => GetSpentTime(x));
                int hours = 0;
                int minutes = 0;
                foreach (var item2 in times)
                {
                    hours += item2.Hours;
                    minutes += item2.Minutes;
                }

                hours += minutes / 60;
                newTypes.Add(new StatisticType(item.Key, hours.ToString(), (minutes % 60).ToString(), $"{hours}:{minutes % 60}", sumHoursInProc, newTypes.Count));
            }

            Types = new ObservableCollection<StatisticType>(newTypes);
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

        private void SetTypes(string period)
        {
            var dateLimit = DateTime.Now;
            var dateMin = DateTime.Now;
            List<DailyTaskModel> dailyTasks = new List<DailyTaskModel>();
            switch (period)
            {
                case Periods.Week:
                    if (dateLimit.DayOfWeek == DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    while (dateLimit.DayOfWeek != DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }
                    dailyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date < dateMin && x.Day.Date > dateLimit).Select(x => new DailyTaskModel(x)).ToList();
                    break;

                case Periods.Month:
                    if (dateLimit.Day == 1)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }
                    dailyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date < dateMin && x.Day.Date > dateLimit).Select(x => new DailyTaskModel(x)).ToList();
                    break;

                case Periods.Quarter:
                    if (dateLimit.Month == 3 || dateLimit.Month == 6 || dateLimit.Month == 9 || dateLimit.Month == 12)
                    {
                        dateLimit = dateLimit.AddMonths(-2);
                    }

                    while (dateLimit.Month != 3 && dateLimit.Month != 6 && dateLimit.Month != 9 && dateLimit.Month != 12)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    dailyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date < dateMin && x.Day.Date > dateLimit).Select(x => new DailyTaskModel(x)).ToList();
                    break;

                case Periods.Year:
                    if (dateLimit.Day == 1 && dateLimit.Month == 1)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(-1);
                    }

                    while (dateLimit.Month != 1)
                    {
                        dateLimit = dateLimit.AddMonths(-1);
                    }

                    dailyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date < dateMin && x.Day.Date > dateLimit).Select(x => new DailyTaskModel(x)).ToList();
                    break;

                case Periods.More:
                    dailyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date < dateMin && x.Day.Date > Periods.TemplateDateTime).Select(x => new DailyTaskModel(x)).ToList();
                    break;
            }
            DateShortFrom = dateLimit.ToShortDateString();
            DateLongFrom = dateLimit.ToLongDateString();
            DateShortTo = dateMin.ToShortDateString();
            DateLongTo = dateMin.ToLongDateString();

            CalculateTypes(dailyTasks.GroupBy(x => x.Type));
        }
    }
}
