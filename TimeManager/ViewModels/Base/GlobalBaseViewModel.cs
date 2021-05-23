using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Abstracts;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public abstract class GlobalBaseViewModel : BaseViewModel
    {
        protected string selectedGlobalPeriod;
        private ComboBoxItem selectedStatus = new ComboBoxItem() { Content = Statuses.All, };

        public virtual List<string> GlobalPeriods { get; set; } = new List<string>()
            { Periods.Week, Periods.Month, Periods.Quarter, Periods.Year, Periods.More, Periods.Previous, Periods.Templates };
        public virtual string SelectedGlobalPeriod { get { return selectedGlobalPeriod; } set { selectedGlobalPeriod = value; SetGlobalTasks(value, true); } }
        public List<GlobalTaskModel> GlobalTasks { get; set; }
        public GlobalTaskModel SelectedGlobalTask { get; set; }
        public string DateShortFrom { get; set; }
        public string DateLongFrom { get; set; }
        public string DateShortTo { get; set; }
        public string DateLongTo { get; set; }

        public ObservableCollection<ComboBoxItem> TaskStatuses { get; set; } = new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem() { Content = Statuses.InProgress, },
                new ComboBoxItem() { Content = Statuses.Done, },
                new ComboBoxItem() { Content = Statuses.NotDone, },
                new ComboBoxItem() { Content = Statuses.All, },
            };

        public ComboBoxItem SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                SetStatus(value);
            }
        }

        public GlobalBaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectedGlobalPeriod = GlobalPeriods.First();
            SelectedStatus = TaskStatuses.FirstOrDefault(x => x.Content.ToString() == Statuses.All);
        }

        public void LoadGlobalTasks()
        {
            SetGlobalTasks(SelectedGlobalPeriod, true);
        }

        protected void SetGlobalTasks(string period, bool isFuture, bool loadStatus = true)
        {
            int mult = 1;
            if (isFuture == false)
            {
                mult *= -1;
            }
            var dateLimit = DateTime.Now;
            var dateMin = DateTime.Now;
            var globalTasks = new List<GlobalTask>();
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
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
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
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
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
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
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
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin, dateLimit)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
                    break;

                case Periods.More:
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, dateMin)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
                    DateShortFrom = dateMin.ToShortDateString();
                    DateLongFrom = dateMin.ToLongDateString();
                    DateShortTo = Texts.Infinity;
                    DateLongTo = Texts.Infinity;
                    LoadStatus(loadStatus);
                    return;

                case Periods.Previous:
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => CheckDates(x.DeadLine, Periods.TemplateDateTime, dateMin)).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
                    DateShortFrom = Periods.TemplateDateTime.ToShortDateString();
                    DateLongFrom = Periods.TemplateDateTime.ToLongDateString();
                    DateShortTo = dateMin.ToShortDateString();
                    DateLongTo = dateMin.ToLongDateString();
                    LoadStatus(loadStatus);
                    return;

                case Periods.Future:
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine > dateMin).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
                    DateShortFrom = Texts.Infinity;
                    DateLongFrom = Texts.Infinity;
                    DateShortTo = dateMin.ToShortDateString();
                    DateLongTo = dateMin.ToLongDateString();
                    LoadStatus(loadStatus);
                    return;

                case Periods.Templates:
                    globalTasks = _globalTaskStorage.GetAllGlobalTasks().Where(x => x.DeadLine == Periods.TemplateDateTime).ToList();
                    GlobalTasks = globalTasks.Select(x => new GlobalTaskModel(x, globalTasks.IndexOf(x))).ToList();
                    DateShortFrom = Texts.None;
                    DateLongFrom = Texts.None;
                    DateShortTo = Texts.None;
                    DateLongTo = Texts.None;
                    LoadStatus(loadStatus);
                    return;
            }
            
            DateShortFrom = dateMin.ToShortDateString();
            DateLongFrom = dateMin.ToLongDateString();
            DateShortTo = dateLimit.ToShortDateString();
            DateLongTo = dateLimit.ToLongDateString();

            LoadStatus(loadStatus);
        }

        private void SetStatus(ComboBoxItem value)
        {
            if (value.Content.ToString() == Statuses.InProgress)
            {
                SetGlobalTasks(SelectedGlobalPeriod, true, false);
                GlobalTasks = GlobalTasks.Where(x => x.Status == Statuses.InProgress).ToList();
            }
            else if (value.Content.ToString() == Statuses.Done)
            {
                SetGlobalTasks(SelectedGlobalPeriod, true, false);
                GlobalTasks = GlobalTasks.Where(x => x.Status == Statuses.Done).ToList();
            }
            else if (value.Content.ToString() == Statuses.NotDone)
            {
                SetGlobalTasks(SelectedGlobalPeriod, true, false);
                GlobalTasks = GlobalTasks.Where(x => x.Status == Statuses.NotDone).ToList();
            }
            else
            {
                SetGlobalTasks(SelectedGlobalPeriod, true, false);
            }
        }

        private void LoadStatus(bool loadStatus)
        {
            if (loadStatus == true)
            {
                SetStatus(SelectedStatus);
            }
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
