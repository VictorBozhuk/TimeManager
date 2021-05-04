using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
using TimeManager.Storage.Storages;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IDayStorage dayStorage;
        private DayModel _selectedDay;
        public ObservableCollection<MyTaskModel> MyTasks { get; set; }
        public ObservableCollection<DayModel> ListOfDays { get; set; }
        public ObservableCollection<WeekModel> ListOfWeeks { get; set; }
        public ObservableCollection<MonthModel> ListOfMonths { get; set; }
        public ObservableCollection<YearModel> ListOfYears { get; set; }
        public ObservableCollection<MyTaskModel> MyPlans { get; set; }
        public MyTaskModel SelectedMyTask { get; set; }
        public MyTaskModel SelectedMyPlan { get; set; }
        public DayModel SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                _selectedDay = value;
                MyPlans = new ObservableCollection<MyTaskModel>(value.Plans);
                MyTasks = new ObservableCollection<MyTaskModel>(value.Tasks);
            }
        }

        public MainPageViewModel(IDayStorage dayStorage)
        {
            this.dayStorage = dayStorage;
            ListOfDays = new ObservableCollection<DayModel>(this.dayStorage.GetAllDays().Select(x => new DayModel(x)).ToList());
            var todayPlans = ListOfDays.FirstOrDefault(x => x.DateShortString == DateTime.Now.ToShortDateString())?.Plans;
            if(todayPlans != null)
            {
                MyPlans = new ObservableCollection<MyTaskModel>(todayPlans);
            }

            ShowTasksOfDayCommand = new RelayCommand(ShowTasksOfSelectedDay);
        }

        #region Commands
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand EstimateCommand { get; set; }
        public RelayCommand GetAllTasksCommand { get; set; }
        public RelayCommand ShowTasksOfDayCommand { get; set; }
        #endregion
        private void ShowTasksOfSelectedDay()
        {
            MyTasks = new ObservableCollection<MyTaskModel>(SelectedDay.Tasks);
        }
    }
}
