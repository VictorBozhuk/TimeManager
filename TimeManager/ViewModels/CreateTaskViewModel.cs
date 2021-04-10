using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeManager.Models;
using TimeManager.Persistence.Arguments;
using TimeManager.Persistence.Repository;

namespace TimeManager.ViewModels
{
    public class CreateTaskViewModel : BaseViewModel
    {
        private IMyTaskStorage myTaskStorage;
        public MyTaskModel MyTask { get; set; }
        private CreateMyTasksViewModel viewModel;
        public CreateTaskViewModel(IMyTaskStorage myTaskStorage, DateTime date, string type, CreateMyTasksViewModel viewModel)
        {
            this.myTaskStorage = myTaskStorage;
            MyTask = new MyTaskModel() { Date = date.ToShortDateString(), Type = type };
            this.viewModel = viewModel;
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          myTaskStorage.Create(CreateMyTaskArgs());
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                          return;
                      }
                      ResetMyTask();
                      MessageBox.Show("Success!");
                      viewModel.UpdateListOfTasks();
                  }));
            }
        }

        public void ResetMyTask()
        {
            MyTask.Name = null;
            MyTask.Date = null;
            MyTask.End = null;
            MyTask.Start = null;
        }

        public MyTaskArgs CreateMyTaskArgs()
        {
            if (string.IsNullOrEmpty(MyTask.Name))
            {
                throw new Exception("Please, enter name.");
            }
            if (string.IsNullOrEmpty(MyTask.Start))
            {
                throw new Exception("Please, enter start corectly.");
            }
            else
            {
                try
                {
                    var date = Convert.ToDateTime($"{MyTask.Date} {MyTask.Start}");
                }
                catch (Exception)
                {
                    throw new Exception("Please, enter start corectly.");
                }
            }
            if (string.IsNullOrEmpty(MyTask.End))
            {
                throw new Exception("Please, enter end corectly.");
            }
            else
            {
                try
                {
                    var date = Convert.ToDateTime($"{MyTask.Date} {MyTask.End}");
                }
                catch (Exception)
                {
                    throw new Exception("Please, enter end corectly.");
                }
            }
            return new MyTaskArgs()
            {
                Name = MyTask.Name,
                Type = MyTask.Type,
                Date = Convert.ToDateTime(MyTask.Date),
                Start = Convert.ToDateTime($"{MyTask.Date} {MyTask.Start}"),
                End = Convert.ToDateTime($"{MyTask.Date} {MyTask.End}"),
            };
        }
    }
}
