using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditDailyTaskViewModel : CreateEditBase
    {
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

        public DailyTaskModel DailyTask { get; set; }

        public CreateEditDailyTaskViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
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

        public void SetGlobalTask(GlobalTaskModel globalTask)
        {
            Load();
            DailyTask = new DailyTaskModel(globalTask);
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
                    Description = DailyTask.Description,
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
                    Description = DailyTask.Description,
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
            LoadTypes();
        }

    }
}
