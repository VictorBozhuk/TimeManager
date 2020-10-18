using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Persistence.Arguments;
using TimeManager.Persistence.Repository;

namespace TimeManager.ViewModels
{
    public class EstimateViewModel : BaseViewModel
    {
        private readonly IMyTaskStorage myTaskStorage;
        public MyTaskModel MyTaskModel { get; set; }

        public readonly ListOfMyTasksViewModel listOfMyTasksViewModel;

        public EstimateViewModel(IMyTaskStorage myTaskStorage, string id, ListOfMyTasksViewModel viewModel)
        {
            this.myTaskStorage = myTaskStorage;
            MyTaskModel = GetMyTaskModel(id);
            listOfMyTasksViewModel = viewModel;
        }

        private MyTaskModel GetMyTaskModel(string id)
        {
            var myTask = myTaskStorage.GetMyTask(id);
            if (myTask != null)
            {
                MyTaskModel = new MyTaskModel(myTask);

                return MyTaskModel;

            }
            return null;
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    myTaskStorage.Edit(CreateMyTaskArgs());
                    listOfMyTasksViewModel.EstimatePage = null;
                });
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    listOfMyTasksViewModel.EstimatePage = null;
                });
            }
        }

        public MyTaskArgs CreateMyTaskArgs()
        {
            return new MyTaskArgs()
            {
                Id = MyTaskModel.Id.ToString(),
                Name = MyTaskModel.Name,
                Type = MyTaskModel.Type,
                Mark = MyTaskModel.SelectedMark.Content.ToString(),
                Date = Convert.ToDateTime(MyTaskModel.Date),
                Start = Convert.ToDateTime($"{MyTaskModel.Date} {MyTaskModel.Start}"),
                End = Convert.ToDateTime($"{MyTaskModel.Date} {MyTaskModel.End}"),
            };
        }
    }
}
