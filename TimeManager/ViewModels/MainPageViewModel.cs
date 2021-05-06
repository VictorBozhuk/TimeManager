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
        private readonly IDayStorage _dayStorage;
        private readonly IMyTaskStorage _myTaskStorage;
        private MainViewModel _main;
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
                if(value != null)
                {
                    _selectedDay = value;
                    MyPlans = new ObservableCollection<MyTaskModel>(value.Plans);
                    MyTasks = new ObservableCollection<MyTaskModel>(value.Tasks);
                }
            }
        }

        public MainPageViewModel(MainViewModel main, IDayStorage dayStorage, IMyTaskStorage myTaskStorage)
        {
            _dayStorage = dayStorage;
            _myTaskStorage = myTaskStorage;
            _main = main;
            LoadDays();
            ShowTasksOfDayCommand = new RelayCommand(ShowTasksOfSelectedDay);
            CreateEditPlanCommand = new RelayCommand(EditPlansOfDay);
            CreateEditTaskCommand = new RelayCommand(EditTasksOfDay);
        }

        #region Commands
        public RelayCommand CreateEditPlanCommand { get; set; }
        public RelayCommand CreateEditTaskCommand { get; set; }
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

        private void GoToCreateEditPlan()
        {

        }

        public void LoadDays()
        {
            ListOfDays = new ObservableCollection<DayModel>(_dayStorage.GetAllDays().Select(x => new DayModel(x)).ToList());
            SelectedDay = ListOfDays.OrderBy(x => x.Date).FirstOrDefault();
        }

        private void EditPlansOfDay()
        {
            _main.MainFrame = new CreateEditDay(_main);
            _main.CreateEditDayVM = new CreateEditDayViewModel(_main, _dayStorage, _myTaskStorage, SelectedDay, true);
        }

        private void EditTasksOfDay()
        {
            _main.MainFrame = new CreateEditDay(_main);
            _main.CreateEditDayVM = new CreateEditDayViewModel(_main, _dayStorage, _myTaskStorage, SelectedDay, false);
        }
    }
}
