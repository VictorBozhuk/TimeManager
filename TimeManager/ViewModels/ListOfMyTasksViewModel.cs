using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Persistence.Arguments;
using TimeManager.Persistence.Repository;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    public class ListOfMyTasksViewModel : BaseViewModel
    {
        private readonly IMyTaskStorage myTaskStorage;
        private Page estimatePage;
        public MyTaskModel SelectedMyTask { get; set; }
        public string SelectedDay { get; set; }
        public ObservableCollection<MyTaskModel> MyTasks { get; set; }
        public ObservableCollection<string> ListOfDays { get; set; }

        public ListOfMyTasksViewModel(IMyTaskStorage myTaskStorage)
        {
            this.myTaskStorage = myTaskStorage;
            ListOfDays = new ObservableCollection<string>(this.myTaskStorage.GetAllMyTasks().Select(x => x.Date).OrderByDescending(x => x).Select(x => x.ToLongDateString()).Distinct());
            if (!myTaskStorage.GetAllMyTasks().Any())
            {
                myTaskStorage.Create(new MyTaskArgs() { Name = "Name1", Type = "type1", Mark = "mark1", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                myTaskStorage.Create(new MyTaskArgs() { Name = "Name2", Type = "type2", Mark = "mark2", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                myTaskStorage.Create(new MyTaskArgs() { Name = "Name3", Type = "type3", Mark = "mark3", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                myTaskStorage.Create(new MyTaskArgs() { Name = "Name4", Type = "type4", Mark = "mark4", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
            }
            MyTasks = new ObservableCollection<MyTaskModel>(GetMyTasks());
        }

        private List<MyTaskModel> GetMyTasks()
        {
            var myTasks = myTaskStorage.GetAllMyTasks();
            return myTasks.Select(x => new MyTaskModel(x, myTasks.IndexOf(x))).ToList();
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if(SelectedMyTask != null)
                    {
                        myTaskStorage.Delete(SelectedMyTask.Id);
                        MyTasks.Remove(MyTasks.First(x => x.Id == SelectedMyTask.Id));
                    }
                });
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    // Go to next page
                });
            }
        }

        public RelayCommand EstimateCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    EstimatePage = new Estimate(myTaskStorage, SelectedMyTask.Id, this);
                });
            }
        }

        public RelayCommand GetAllTasksCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MyTasks = new ObservableCollection<MyTaskModel>(GetMyTasks());
                    OnPropertyChanged(nameof(MyTasks));
                });
            }
        }

        public RelayCommand DayFromListDoubleClickCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    var newMyTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == SelectedDay).ToList();
                    MyTasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
                    OnPropertyChanged(nameof(MyTasks));
                });
            }
        }

        public Page EstimatePage
        {
            get { return estimatePage; }
            set
            {
                estimatePage = value;
                OnPropertyChanged(nameof(EstimatePage));
            }
        }



    }
}
