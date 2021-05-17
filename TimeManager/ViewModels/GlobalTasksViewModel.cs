using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GlobalTasksViewModel : GlobalBaseViewModel
    {
        public GlobalTasksViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage) 
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            DeleteGlobalTaskCommand = new RelayCommand(DeleteGlobalTask);
            EditTaskCommand = new RelayCommand(EditGlobalTask);
        }

        public RelayCommand DeleteGlobalTaskCommand { get; set; }
        public RelayCommand EditTaskCommand { get; set; }

        private void DeleteGlobalTask()
        {
            _globalTaskStorage.Delete(SelectedGlobalTask.Id.ToString());
            LoadGlobalTasks();
        }

        private void EditGlobalTask()
        {
            var isTemplate = SelectedGlobalPeriod == Periods.Templates;
            _main.MainFrame = new CreateEditGlobalTask(_main);
            _main.CreateEditGlobalTaskVM = new CreateEditGlobalTaskViewModel(_main, _dayStorage, _dailyTaskStorage, _globalTaskStorage, SelectedGlobalTask, isTemplate);
        }

    }
}
