using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstract;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GlobalTemplatesViewModel : GlobalBaseViewModel
    {
        public GlobalTemplatesViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            SelectGlobalTaskCommand = new RelayCommand(SelectGlobalTask);
        }

        public RelayCommand SelectGlobalTaskCommand { get; set; }

        private void SelectGlobalTask()
        {
            _main.CreateEditDayVM.CreateEditDailyTaskVM.SetGlobalTask(SelectedGlobalTask);
            _main.CreateEditDayVM.SelectedTemplateTab = Texts.CreateForm;
        }
    }
}
