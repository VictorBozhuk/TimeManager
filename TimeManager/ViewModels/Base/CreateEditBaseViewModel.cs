using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Abstracts;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]

    public abstract class CreateEditBaseViewModel : BaseViewModel
    {
        protected readonly IDailyTaskStorage _dailyTaskStorage;
        protected readonly IGlobalTaskStorage _globalTaskStorage;
        protected readonly IDayStorage _dayStorage;
        protected MainViewModel _main;

        public CreateEditBaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            _dailyTaskStorage = dailyTaskStorage;
            _dayStorage = dayStorage;
            _globalTaskStorage = globalTaskStorage;
            _main = main;
        }

        public string NamePage { get; set; } = Texts.Create;
        public ObservableCollection<ComboBoxItem> Types { get; set; }
        public ObservableCollection<ComboBoxItem> TaskStatuses { get; set; }
            = new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem() { Content = Statuses.InProgress, },
                new ComboBoxItem() { Content = Statuses.Done, },
                new ComboBoxItem() { Content = Statuses.NotDone, },
            };


        protected void LoadTypes()
        {
            var typesFromDailyTasks = _dailyTaskStorage.GetAllDailyTasks().Select(x => x.Type);
            var typesFromGlobaltasks = _globalTaskStorage.GetAllGlobalTasks().Select(x => x.Type).ToList();
            typesFromGlobaltasks.AddRange(typesFromDailyTasks);
            Types = new ObservableCollection<ComboBoxItem>(typesFromGlobaltasks.Distinct().Select(x => new ComboBoxItem() { Content = x }));
        }
    }
}
