using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditTaskViewModel
    {
        private readonly IMyTaskStorage _myTaskStorage;
        private readonly IDayStorage _dayStorage;
        private MainViewModel _main;
        public string NamePage { get; set; } = "Create";
        public bool IsCreate { get; set; } = true;
        public ComboBoxItem SelectedType
        {
            set
            {
                if(value != null)
                {
                    Task.Type = value.Content.ToString();
                }
            }
        }
        public ObservableCollection<ComboBoxItem> Types { get; set; }

        public MyTaskModel Task { get; set; }

        public CreateEditTaskViewModel(MainViewModel main, IDayStorage dayStorage, IMyTaskStorage myTaskStorage)
        {
            _myTaskStorage = myTaskStorage;
            _dayStorage = dayStorage;
            _main = main;
            Load();
            ExecuteCommand = new RelayCommand(Execute);
            CancelCommand = new RelayCommand(Load);
        }

        public RelayCommand ExecuteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void Execute()
        {
            var day = _main.CreateEditDayVM.Day.Date.ToShortDateString();
            if(_dayStorage.GetAllDays().Any(x => x.Date.ToShortDateString() == day))
            {
                var taskArgs = new MyTaskArgs()
                {
                    Title = Task.Title,
                    Type = Task.Type,
                    Start = Task.Start,
                    End = Task.End,
                    DayId = _main.CreateEditDayVM.Day.Id,
                    IsPlan = _main.CreateEditDayVM.PlansVisible,
                };
                _myTaskStorage.Create(taskArgs);
            }
            else
            {
                var newDay = new DayArgs()
                {
                    Date = _main.CreateEditDayVM.Day.Date,
                };

                _dayStorage.Create(newDay);
                _main.CreateEditDayVM.RefreshDay();

                var taskArgs = new MyTaskArgs()
                {
                    Title = Task.Title,
                    Type = Task.Type,
                    Start = Task.Start,
                    End = Task.End,
                    DayId = _main.CreateEditDayVM.Day.Id,
                    IsPlan = _main.CreateEditDayVM.PlansVisible,
                };
                _myTaskStorage.Create(taskArgs);
            }
            Load();
            _main.CreateEditDayVM.ShowTasks();
        }

        private void Load()
        {
            Task = new MyTaskModel();
            Types = new ObservableCollection<ComboBoxItem>(_myTaskStorage.GetAllMyTasks().Select(x => x.Type).Distinct().Select(x => new ComboBoxItem() { Content = x }));
        }

    }
}
