using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Models;
using TimeManager.Models.StatisticModels;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class StatisticViewModel : BaseViewModel
    {
        private ComboBoxItem selectedStatisticPage;
        public List<ComboBoxItem> StatisticPages { get; set; }
        public ComboBoxItem SelectedStatisticPage
        {
            get { return selectedStatisticPage; }
            set 
            {
                selectedStatisticPage = value;
                if (value.Content.ToString() == Texts.GlobalTasks)
                {
                    StatisticPage = new StatisticOfGlobalTasks(_main);
                }
                else if (value.Content.ToString() == Texts.Types)
                {
                    StatisticPage = new StatisticOfTypes(_main);
                }
            } 
        }
        public StatisticGlobalTasksViewModel StatisticGlobalTasksVM { get; set; }
        public StatisticTypesViewModel StatisticTypesVM { get; set; }
        public Page StatisticPage { get; set; }
        public StatisticViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            StatisticGlobalTasksVM = new StatisticGlobalTasksViewModel(main, dayStorage, dailyTaskStorage, globalTaskStorage);
            StatisticTypesVM = new StatisticTypesViewModel(main, dayStorage, dailyTaskStorage, globalTaskStorage);
            StatisticPages = new List<ComboBoxItem>() { new ComboBoxItem() { Content = Texts.GlobalTasks, }, new ComboBoxItem() { Content = Texts.Types } };
            SelectedStatisticPage = StatisticPages.First();
        }
    }
}
