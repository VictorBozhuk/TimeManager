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
        public List<string> GlobalPeriods { get; set; } = new List<string>()
            { Periods.Week, Periods.TwoWeeks, Periods.Month, Periods.Quarter, Periods.HalfYear, Periods.Year, Periods.OverYear, Periods.Templates };
        public string SelectedGlobalPeriod { get { return selectedGlobalPeriod; } set { selectedGlobalPeriod = value; SetGlobalTasks(value); } }

        public List<GlobalTaskModel> GlobalTasks { get; set; }
        public GlobalTaskModel SelectedGlobalTask { get; set; }

        public GlobalBaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectedGlobalPeriod = GlobalPeriods.First();
        }

        public void LoadGlobalTasks()
        {
            SetGlobalTasks(SelectedGlobalPeriod);
        }

        private void SetGlobalTasks(string period)
        {
            var dateLimit = DateTime.Now;
            var dateMin = new DateTime(2000, 1, 1);
            switch (period)
            {
                case Periods.Week:
                    while (dateLimit.DayOfWeek != DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.TwoWeeks:
                    while (dateLimit.DayOfWeek != DayOfWeek.Monday)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }

                    do
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }
                    while (dateLimit.DayOfWeek != DayOfWeek.Monday);
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Month:
                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Quarter:
                    if (dateLimit.Month == 3 || dateLimit.Month == 6 || dateLimit.Month == 9 || dateLimit.Month == 12)
                    {
                        dateLimit = dateLimit.AddMonths(2);
                    }

                    while (dateLimit.Month != 3 && dateLimit.Month != 6 && dateLimit.Month != 9 && dateLimit.Month != 12)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.HalfYear:
                    if (dateLimit.Month == 1 || dateLimit.Month == 7)
                    {
                        dateLimit = dateLimit.AddMonths(5);
                    }

                    while (dateLimit.Month != 1 && dateLimit.Month != 7)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Year:
                    while (dateLimit.Day != 1)
                    {
                        dateLimit = dateLimit.AddDays(1);
                    }

                    while (dateLimit.Month != 1)
                    {
                        dateLimit = dateLimit.AddMonths(1);
                    }

                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin && x.DeadLine < dateLimit).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.OverYear:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin).Select(x => new GlobalTaskModel(x)).ToList();
                    break;

                case Periods.Templates:
                    GlobalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine == Periods.TemplateDateTime).Select(x => new GlobalTaskModel(x)).ToList();
                    break;
            }
        }
    }
}
