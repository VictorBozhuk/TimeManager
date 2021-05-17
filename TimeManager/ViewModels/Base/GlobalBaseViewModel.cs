using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public abstract class GlobalBaseViewModel : BaseViewModel
    {
        protected string selectedGlobalPeriod;
        public virtual List<string> GlobalPeriods { get; set; } = new List<string>()
            { Periods.Week, Periods.TwoWeeks, Periods.Month, Periods.Quarter, Periods.HalfYear, Periods.Year, Periods.OverYear, Periods.Previous, Periods.Templates };
        public virtual string SelectedGlobalPeriod { get { return selectedGlobalPeriod; } set { selectedGlobalPeriod = value; SetGlobalTasks(value, true); } }
        public List<GlobalTaskModel> GlobalTasks { get; set; }
        public GlobalTaskModel SelectedGlobalTask { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public GlobalBaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectedGlobalPeriod = GlobalPeriods.First();
        }

        public void LoadGlobalTasks()
        {
            SetGlobalTasks(SelectedGlobalPeriod, true);
        }

        protected void SetGlobalTasks(string period, bool isFuture)
        {
            int mult = 1;
            if (isFuture == false)
            {
                mult *= -1;
            }
            var dateLimit = DateTime.Now;
            var dateMin = DateTime.Now;
            switch (period)
            {
                case Periods.Week:
                    if(dateLimit.DayOfWeek == DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.DayOfWeek != DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.TwoWeeks:
                    if (dateLimit.DayOfWeek == DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.DayOfWeek != DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    do
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }
                    while (dateLimit.DayOfWeek != DayOfWeek.Monday);
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Month:
                    if (dateLimit.Day == 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Quarter:
                    if (dateLimit.Month == 3 || dateLimit.Month == 6 || dateLimit.Month == 9 || dateLimit.Month == 12)
                    {
                        dateLimit = dateLimit.AddMonths(2 * mult);
                    }

                    while (dateLimit.Month != 3 && dateLimit.Month != 6 && dateLimit.Month != 9 && dateLimit.Month != 12)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.HalfYear:
                    if (dateLimit.Month == 1 || dateLimit.Month == 7)
                    {
                        dateLimit = dateLimit.AddMonths(5 * mult);
                    }

                    while (dateLimit.Month != 1 && dateLimit.Month != 7)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Year:
                    if(dateLimit.Day == 1 && dateLimit.Month == 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1 * mult);
                    }

                    while (dateLimit.Month != 1)
                    {
                        dateLimit = dateLimit.AddMonths(1 * mult);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.OverYear:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin)).Select(x => new GlobalTaskModel(x)).ToList();
                    DateFrom = dateMin.ToShortDateString();
                    DateTo = Texts.Infinity;
                    return;

                case Periods.Previous:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, Periods.TemplateDateTime, dateMin)).Select(x => new GlobalTaskModel(x)).ToList();
                    DateFrom = Periods.TemplateDateTime.ToShortDateString();
                    DateTo = dateMin.ToShortDateString();
                    return;

                case Periods.Future:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin).Select(x => new GlobalTaskModel(x)).ToList();
                    DateFrom = Texts.Infinity;
                    DateTo = dateMin.ToShortDateString();
                    return;

                case Periods.Templates:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine == Periods.TemplateDateTime).Select(x => new GlobalTaskModel(x)).ToList();
                    DateFrom = Texts.None;
                    DateTo = Texts.None;
                    return;
            }

            DateFrom = dateMin.ToShortDateString();
            DateTo = dateLimit.ToShortDateString();
        }

        protected virtual bool CheckDates(DateTime deadLine, DateTime dateMin, DateTime dateLimit)
        {
            return deadLine > dateMin && deadLine < dateLimit;
        }

        protected virtual bool CheckDates(DateTime deadLine, DateTime dateMin)
        {
            return deadLine > dateMin;
        }
    }
}
