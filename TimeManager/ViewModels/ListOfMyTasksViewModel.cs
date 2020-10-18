using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Persistence.Repository;

namespace TimeManager.ViewModels
{
    public class ListOfMyTasksViewModel : BaseViewModel
    {
        IMyTaskStorage myTaskStorage;
        public MyTaskModel SelectedMyTask { get; set; }
        public ObservableCollection<MyTaskModel> MyTasks { get; set; }

        public ListOfMyTasksViewModel(IMyTaskStorage myTaskStorage)
        {
            this.myTaskStorage = myTaskStorage;
            MyTasks = new ObservableCollection<MyTaskModel>(GetMyTasks());
        }

        private List<MyTaskModel> GetMyTasks()
        {
            var myTasks = myTaskStorage.GetAllMyTasks();
            return myTasks.Select(x => new MyTaskModel(x, myTasks.IndexOf(x))).ToList();
        }
    }
}
