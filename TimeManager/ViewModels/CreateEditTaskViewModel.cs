using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    public class CreateEditTaskViewModel
    {
        private readonly IMyTaskStorage myTaskStorage;
        public string NamePage { get; set; } = "Create";
        public bool IsCreate { get; set; } = true;
        public string Type { get; set; }
        public string SelectedType { get; set; }
        public ObservableCollection<string> Types { get; set; }

        public MyTask Task { get; set; }

        public CreateEditTaskViewModel(IMyTaskStorage myTaskStorage)
        {
            this.myTaskStorage = myTaskStorage;
            Types = new ObservableCollection<string>(myTaskStorage.GetAllMyTasks().Select(x => x.Type).Distinct());
            ExecuteCommand = new RelayCommand(Execute);
        }

        public RelayCommand ExecuteCommand { get; set; }


        private void Execute()
        {
            // натискаю на кнопку Create;
        }

    }
}
