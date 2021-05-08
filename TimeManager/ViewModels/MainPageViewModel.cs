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
        private readonly IDailyTaskStorage _dailyTaskStorage;
        private MainViewModel _main;
        private DayModel _selectedDay;
        public ObservableCollection<DailyTaskModel> DailyTasks { get; set; }
        public ObservableCollection<DayModel> ListOfDays { get; set; }
        public ObservableCollection<WeekModel> ListOfWeeks { get; set; }
        public ObservableCollection<MonthModel> ListOfMonths { get; set; }
        public ObservableCollection<YearModel> ListOfYears { get; set; }
        public ObservableCollection<DailyTaskModel> DailyPlans { get; set; }
        public DailyTaskModel SelectedDailyTask { get; set; }
        public DailyTaskModel SelectedDailyPlan { get; set; }
        public DayModel SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if(value != null)
                {
                    _selectedDay = value;
                    DailyPlans = new ObservableCollection<DailyTaskModel>(value.DailyPlans);
                    DailyTasks = new ObservableCollection<DailyTaskModel>(value.DailyTasks);
                }
            }
        }

        public MainPageViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage)
        {
            _dayStorage = dayStorage;
            _dailyTaskStorage = dailyTaskStorage;
            _main = main;
            LoadDays();
            ShowDailyTasksOfDayCommand = new RelayCommand(ShowDailyTasksOfSelectedDay);
            CreateEditDailyPlanCommand = new RelayCommand(() => EditDailyTasksOfDay(true));
            CreateEditDailyTaskCommand = new RelayCommand(() => EditDailyTasksOfDay(false));
            DeleteDayCommand = new RelayCommand(DeleteDay);
            DeleteDailyPlanCommand = new RelayCommand(() => DeleteDailyTask(SelectedDailyPlan, SelectedDay.DailyPlans, DailyPlans));
            DeleteDailyTaskCommand = new RelayCommand(() => DeleteDailyTask(SelectedDailyTask, SelectedDay.DailyTasks, DailyTasks));
            EditDailyPlanCommand = new RelayCommand(() => EditDailyTask(true, SelectedDailyPlan));
            EditDailyTaskCommand = new RelayCommand(() => EditDailyTask(false, SelectedDailyTask));
        }

        #region Commands
        public RelayCommand CreateEditDailyPlanCommand { get; set; }
        public RelayCommand CreateEditDailyTaskCommand { get; set; }
        public RelayCommand GetAllDailyTasksCommand { get; set; }
        public RelayCommand ShowDailyTasksOfDayCommand { get; set; }
        public RelayCommand DeleteDayCommand { get; set; }
        public RelayCommand EditDailyPlanCommand { get; set; }
        public RelayCommand DeleteDailyPlanCommand { get; set; }
        public RelayCommand EditDailyTaskCommand { get; set; }
        public RelayCommand DeleteDailyTaskCommand { get; set; }
        #endregion
        private void ShowDailyTasksOfSelectedDay()
        {
            DailyTasks = new ObservableCollection<DailyTaskModel>(SelectedDay.DailyTasks);
        }

        private void EditDailyTask(bool isPlans, DailyTaskModel task)
        {
            _main.MainFrame = new CreateEditDay(_main);
            _main.CreateEditDayVM = new CreateEditDayViewModel(_main, _dayStorage, _dailyTaskStorage, SelectedDay, isPlans, task);
        }

        private void DeleteDailyTask(DailyTaskModel selectedTask, List<DailyTaskModel> myTaskModelsFromDay, ObservableCollection<DailyTaskModel> myTaskModels)
        {
            _dailyTaskStorage.Delete(selectedTask.Id);
            if (_dayStorage.GetAllDays().FirstOrDefault(x => x.Date == selectedTask.Day.Date).DailyTasks.Count == 0)
            {
                _dayStorage.Delete(selectedTask.Day.Id.ToString());
                LoadDays();
            }
            else
            {
                myTaskModelsFromDay.RemoveAll(x => x.Id == selectedTask.Id);
                myTaskModels.Remove(selectedTask);
            }
        }

        private void DeleteDay()
        {
            _dayStorage.Delete(SelectedDay.Id.ToString());
            LoadDays();
        }

        public void LoadDays()
        {
            ListOfDays = new ObservableCollection<DayModel>(_dayStorage.GetAllDays().Select(x => new DayModel(x)).ToList());
            SelectedDay = ListOfDays.OrderBy(x => x.Date).FirstOrDefault();
        }

        private void EditDailyTasksOfDay(bool isPlans)
        {
            _main.MainFrame = new CreateEditDay(_main);
            _main.CreateEditDayVM = new CreateEditDayViewModel(_main, _dayStorage, _dailyTaskStorage, SelectedDay, isPlans);
        }
    }
}
