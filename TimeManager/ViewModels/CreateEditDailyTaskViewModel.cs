using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditDailyTaskViewModel
    {
        private readonly IDailyTaskStorage _dailyTaskStorage;
        private readonly IDayStorage _dayStorage;
        private MainViewModel _main;
        public string NamePage { get; set; } = Texts.Create;
        public ComboBoxItem SelectedType
        {
            set
            {
                if(value != null)
                {
                    DailyTask.Type = value.Content.ToString();
                }
            }
        }
        public ObservableCollection<ComboBoxItem> Types { get; set; }

        public DailyTaskModel DailyTask { get; set; }

        public CreateEditDailyTaskViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage)
        {
            _dailyTaskStorage = dailyTaskStorage;
            _dayStorage = dayStorage;
            _main = main;
            Load();
            ExecuteCommand = new RelayCommand(Execute);
            CancelCommand = new RelayCommand(Load);
        }

        public RelayCommand ExecuteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public void PrepareEditDailyTask(DailyTaskModel task)
        {
            DailyTask = task;
            NamePage = Texts.Edit;
        }

        private void Execute()
        {
            if(NamePage == Texts.Create)
            {
                CreateDailyTask();
            }
            else if(NamePage == Texts.Edit)
            {
                EditDailyTask();
            }

            Load();
            _main.CreateEditDayVM.ShowDailyTasks();
        }

        private void CreateDailyTask()
        {
            var day = _main.CreateEditDayVM.Day.Date.ToShortDateString();
            if (_dayStorage.GetAllDays().Any(x => x.Date.ToShortDateString() == day))
            {
                var taskArgs = new DailyTaskArgs()
                {
                    Title = DailyTask.Title,
                    Type = DailyTask.Type,
                    Start = DailyTask.Start,
                    End = DailyTask.End,
                    DayId = _main.CreateEditDayVM.Day.Id,
                    IsPlan = _main.CreateEditDayVM.DailyPlansVisible,
                };
                _dailyTaskStorage.Create(taskArgs);
            }
            else
            {
                var newDay = new DayArgs()
                {
                    Date = _main.CreateEditDayVM.Day.Date,
                };

                _dayStorage.Create(newDay);
                _main.CreateEditDayVM.RefreshDay();

                var taskArgs = new DailyTaskArgs()
                {
                    Title = DailyTask.Title,
                    Type = DailyTask.Type,
                    Start = DailyTask.Start,
                    End = DailyTask.End,
                    DayId = _main.CreateEditDayVM.Day.Id,
                    IsPlan = _main.CreateEditDayVM.DailyPlansVisible,
                };
                _dailyTaskStorage.Create(taskArgs);
            }
        }

        private void EditDailyTask()
        {
            var taskArgs = new DailyTaskArgs()
            {
                Id = DailyTask.Id,
                Title = DailyTask.Title,
                Mark = DailyTask.Mark,
                Status = DailyTask.Status,
                Type = DailyTask.Type,
                Start = DailyTask.Start,
                End = DailyTask.End,
                DayId = _main.CreateEditDayVM.Day.Id,
                IsPlan = _main.CreateEditDayVM.DailyPlansVisible,
            };

            _dailyTaskStorage.Edit(taskArgs);
        }

        private void Load()
        {
            NamePage = Texts.Create;
            DailyTask = new DailyTaskModel();
            Types = new ObservableCollection<ComboBoxItem>(_dailyTaskStorage.GetAllDailyTasks().Select(x => x.Type).Distinct().Select(x => new ComboBoxItem() { Content = x }));
        }
    }
}
