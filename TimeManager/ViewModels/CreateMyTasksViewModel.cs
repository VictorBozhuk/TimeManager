using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Persistence.Repository;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    public class CreateMyTasksViewModel : BaseViewModel
    {
        private readonly IMyTaskStorage myTaskStorage;
        private Page taskPage;
        public DateTime pickeredDate;
        public MyTaskModel SelectedMyTask { get; set; }
        public string selectedDay;
        public string Type { get; set; }
        public ObservableCollection<MyTaskModel> MyTasks { get; set; }
        public ObservableCollection<string> ListOfDays { get; set; }



        public CreateMyTasksViewModel(IMyTaskStorage myTaskStorage)
        {
            this.myTaskStorage = myTaskStorage;
            MyTasks = new ObservableCollection<MyTaskModel>(GetMyTasksToday());
            ListOfDays = new ObservableCollection<string>(GetDaysForUpdating());
            PickeredDate = DateTime.Now;
            Type = "Plan";
            TaskPage = new CreateTask(myTaskStorage, Convert.ToDateTime(SelectedDay), Type, this);
        }

        private List<MyTaskModel> GetMyTasksToday()
        {
            var myTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == DateTime.Now.ToLongTimeString()).ToList();
            return myTasks.Select(x => new MyTaskModel(x, myTasks.IndexOf(x))).ToList();
        }

        private List<string> GetDaysForUpdating()
        {
            var Now = DateTime.Now;
            var listOfDays = new List<string>();
            for(int i = 0; i <= 7; ++i)
            {
                listOfDays.Add(Now.ToLongDateString());
                Now = Now.AddDays(1);
            }
            return listOfDays;
        }

        private void ShowTasksInList()
        {
            var newMyTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == SelectedDay).ToList();
            MyTasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
            OnPropertyChanged(nameof(MyTasks));
        }

        public RelayCommand Command
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    
                });
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    //TaskPage = new CreateTask(myTaskStorage, Type, this);
                });
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    myTaskStorage.Delete(SelectedMyTask.Id);
                    MyTasks.Remove(MyTasks.First(x => x.Id == SelectedMyTask.Id));
                });
            }
        }

        public RelayCommand DayFromListDoubleClickCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ShowTasksInList();
                });
            }
        }

        public string SelectedDay
        {
            get { return selectedDay; }
            set
            {
                selectedDay = value;
                OnPropertyChanged(nameof(SelectedDay));
            }
        }
        public Page TaskPage
        {
            get { return taskPage; }
            set
            {
                taskPage = value;
                OnPropertyChanged(nameof(TaskPage));
            }
        }
        public DateTime PickeredDate
        {
            get { return pickeredDate; }
            set
            {
                pickeredDate = value;
                SelectedDay = pickeredDate.ToLongDateString();
                ShowTasksInList();
                OnPropertyChanged(nameof(PickeredDate));
            }
        }

        internal void UpdateListOfTasks()
        {
            var newMyTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == SelectedDay).ToList();
            MyTasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
            OnPropertyChanged(nameof(MyTasks));
        }
    }
}
