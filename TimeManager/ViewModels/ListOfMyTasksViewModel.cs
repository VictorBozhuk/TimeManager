using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.ViewModels
{
    public class ListOfMyTasksViewModel : BaseViewModel
    {

        public MyTaskModel SelectedMyTask { get; set; }
        public ObservableCollection<MyTaskModel> MyTasks { get; set; }

        public ListOfMyTasksViewModel()
        {
            MyTasks = new ObservableCollection<MyTaskModel>(GetMyTasks());
        }

        private List<MyTaskModel> GetMyTasks()
        {
            var myTasks = Resource.getInstance().MyTaskStorage.GetAllMyTasks();
            return myTasks.Select(x => new MyTaskModel(x, myTasks.IndexOf(x))).ToList();
        }
    }
}
