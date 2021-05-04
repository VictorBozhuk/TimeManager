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
    public class CreateDayViewModel : BaseViewModel
    {
        private readonly IMyTaskStorage myTaskStorage;
        public DateTime pickeredDate;
        public MyTaskModel SelectedMyTask { get; set; }
        public string selectedDay;

        public ObservableCollection<MyTaskModel> MyTasks { get; set; }

        public CreateDayViewModel(IMyTaskStorage myTaskStorage)
        {
            this.myTaskStorage = myTaskStorage;
            PickeredDate = DateTime.Now;
        }

        private void ShowTasksInList()
        {
            var newMyTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == SelectedDay).ToList();
            MyTasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }






        public string SelectedDay { get; set; }

        public DateTime PickeredDate
        {
            get { return pickeredDate; }
            set
            {
                pickeredDate = value;
                SelectedDay = pickeredDate.ToLongDateString();
                ShowTasksInList();
            }
        }

        internal void UpdateListOfTasks()
        {
            var newMyTasks = myTaskStorage.GetAllMyTasks().Where(x => x.Date.ToLongDateString() == SelectedDay).ToList();
            MyTasks = new ObservableCollection<MyTaskModel>(newMyTasks.Select(x => new MyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }
    }
}
