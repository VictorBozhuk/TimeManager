using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditDayViewModel : BaseViewModel
    {
        private readonly IMyTaskStorage _myTaskStorage;
        private readonly IDayStorage _dayStorage;
        public DateTime pickeredDate;
        public bool PlansVisible { get; set; }
        public string TypeOfTasks { get; set; }
        public MyTaskModel SelectedMyTask { get; set; }
        public DayModel Day { get; set; }

        public ObservableCollection<MyTaskModel> Tasks { get; set; }
        public Page CreateEditTaskFrame { get; set; }
        public CreateEditTaskViewModel CreateEditTaskVM { get; set; }
        public CreateEditDayViewModel(MainViewModel main, IDayStorage dayStorage, IMyTaskStorage myTaskStorage, DayModel day = null)
        {
            _myTaskStorage = myTaskStorage;
            _dayStorage = dayStorage;
            CreateEditTaskFrame = new CreateEditTask(main);
            CreateEditTaskVM = new CreateEditTaskViewModel(main, dayStorage, myTaskStorage);
            ShowPlansCommand = new RelayCommand(ShowPlans);
            ShowTasksCommand = new RelayCommand(ShowDoneTasks);
            if (day == null)
            {
                var h = dayStorage.GetAllDays();
                var firstDay = h.FirstOrDefault();
                if (firstDay != null && firstDay.Date.ToShortDateString() == DateTime.Now.ToShortDateString() || firstDay.Date > DateTime.Now)
                {
                    Day = new DayModel(firstDay.Date.AddDays(1));
                    PickeredDate = Day.Date;
                }
                else
                {
                    Day = new DayModel(DateTime.Now);
                    PickeredDate = Day.Date;
                }
            }
            else
            {

            }

        }

        public RelayCommand ShowPlansCommand { get; set; }
        public RelayCommand ShowTasksCommand { get; set; }

        public DateTime PickeredDate
        {
            get { return pickeredDate; }
            set
            {
                pickeredDate = value;
                Day.Date = value;
                ShowTasks();
            }
        }

        public void RefreshDay()
        {
            var dayFromStorage = _dayStorage.GetAllDays().FirstOrDefault(x => x.Date.ToShortDateString() == Day.DateShortString);
            Day = new DayModel(dayFromStorage);
        }

        public void ShowTasks()
        {
            if (PlansVisible)
            {
                ShowPlans();
            }
            else
            {
                ShowDoneTasks();
            }
        }

        private void ShowDoneTasks()
        {
            TypeOfTasks = "Tasks";
            PlansVisible = false;
            var newMyTasks = _myTaskStorage.GetAllMyTasks().Where(x => x.Day.Date.ToShortDateString() == Day.Date.ToShortDateString() && x.IsPlan == false).ToList();
            Tasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }

        private void ShowPlans()
        {
            TypeOfTasks = "Plans";
            PlansVisible = true;
            var newMyTasks = _myTaskStorage.GetAllMyTasks().Where(x => x.Day.Date.ToShortDateString() == Day.Date.ToShortDateString() && x.IsPlan == true).ToList();
            Tasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }
    }
}
