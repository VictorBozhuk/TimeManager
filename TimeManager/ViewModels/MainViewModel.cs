using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Models;
using TimeManager.Storage;
using TimeManager.Storage.Storages;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private TimeManagerDbContext _dbContext;
        private IDayStorage _dayStorage;
        private IDailyTaskStorage _dailyTaskStorage;
        private IGlobalTaskStorage _globalTaskStorage;
        public DailyTasksViewModel DailyTasksVM { get; set; }
        public GlobalTasksViewModel GlobalTasksVM { get; set; }
        public CreateEditDayViewModel CreateEditDayVM { get; set; }
        public CreateEditGlobalTaskViewModel CreateEditGlobalTaskVM { get; set; }

        public Page MainFrame { get; set; }
        public MainViewModel()
        {
            _dbContext = new TimeManagerDbContext();
            _dayStorage = new DayStorage(_dbContext);
            _dailyTaskStorage = new DailyTaskStorage(_dbContext);
            _globalTaskStorage = new GlobalTaskStorage(_dbContext);
            Initializer();
            MainFrame = new DailyTasks(this);
            DailyTasksVM = new DailyTasksViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
            GlobalTasksVM = new GlobalTasksViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
            CreateDayCommand = new RelayCommand(GoToCreateDay);
            GoToDailyTasksCommand = new RelayCommand(GoToDailyTasks);
            GoToGlobalTasksCommand = new RelayCommand(GoToGlobalTasks);
            CreateGlobalTaskCommand = new RelayCommand(GoToCreateGlobalTask);
        }

        public RelayCommand GoToDailyTasksCommand { get; set; }
        public RelayCommand GoToGlobalTasksCommand { get; set; }
        public RelayCommand CreateEditDayCommand { get; set; }
        public RelayCommand CreateDayCommand { get; set; }
        public RelayCommand CreateGlobalTaskCommand { get; set; }

        private void GoToCreateDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
        }

        private void GoToEditDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
        }

        private void GoToDailyTasks()
        {
            MainFrame = new DailyTasks(this);
            DailyTasksVM.LoadDays();
        }
        public void GoToGlobalTasks()
        {
            MainFrame = new GlobalTasks(this);
            GlobalTasksVM.LoadGlobalTasks();
        }
        private void GoToCreateGlobalTask()
        {
            MainFrame = new CreateEditGlobalTask(this);
            CreateEditGlobalTaskVM = new CreateEditGlobalTaskViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
        }


        private void Initializer()
        {
            if(_dayStorage.GetAllDays().Count == 0)
            {
                var day1 = new Day()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                };
                var day2 = new Day()
                {
                    Date = DateTime.Now.AddDays(1),
                    Id = Guid.NewGuid(),
                };
                var day3 = new Day()
                {
                    Date = DateTime.Now.AddDays(2),
                    Id = Guid.NewGuid(),
                };

                var plan1 = GetDailyPlan(day1);
                var plan2 = GetDailyPlan(day1);
                var plan3 = GetDailyPlan(day1);
                var plan4 = GetDailyPlan(day2);
                var plan5 = GetDailyPlan(day2);
                var plan6 = GetDailyPlan(day2);
                var plan7 = GetDailyPlan(day3);
                var plan8 = GetDailyPlan(day3);
                var plan9 = GetDailyPlan(day3);

                var task1 = GetDailyTask(day1);
                var task2 = GetDailyTask(day1);
                var task3 = GetDailyTask(day1);
                var task4 = GetDailyTask(day2);
                var task5 = GetDailyTask(day2);
                var task6 = GetDailyTask(day2);

                var gTask1 = GetGlobalTask(2);
                var gTask2 = GetGlobalTask(3);
                var gTask3 = GetGlobalTask(15);
                var gTask4 = GetGlobalTask(100);

                var gPlan1 = GetGlobalPlan(34);
                var gPlan2 = GetGlobalPlan(45);
                var gPlan3 = GetGlobalPlan(140);
                var gPlan4 = GetGlobalPlan(4);


                _dbContext.Days.Add(day1);
                _dbContext.Days.Add(day2);
                _dbContext.Days.Add(day3);

                _dbContext.DailyTasks.Add(plan1);
                _dbContext.DailyTasks.Add(plan2);
                _dbContext.DailyTasks.Add(plan3);
                _dbContext.DailyTasks.Add(plan4);
                _dbContext.DailyTasks.Add(plan5);
                _dbContext.DailyTasks.Add(plan6);
                _dbContext.DailyTasks.Add(plan7);
                _dbContext.DailyTasks.Add(plan8);
                _dbContext.DailyTasks.Add(plan9);
                _dbContext.DailyTasks.Add(task1);
                _dbContext.DailyTasks.Add(task2);
                _dbContext.DailyTasks.Add(task3);
                _dbContext.DailyTasks.Add(task4);
                _dbContext.DailyTasks.Add(task5);
                _dbContext.DailyTasks.Add(task6);

                _dbContext.GlobalTasks.Add(gTask1);
                _dbContext.GlobalTasks.Add(gTask2);
                _dbContext.GlobalTasks.Add(gTask3);
                _dbContext.GlobalTasks.Add(gTask4);
                _dbContext.GlobalTasks.Add(gPlan1);
                _dbContext.GlobalTasks.Add(gPlan2);
                _dbContext.GlobalTasks.Add(gPlan3);
                _dbContext.GlobalTasks.Add(gPlan4);

                _dbContext.SaveChanges();
            }

        }
        private DailyTask GetDailyTask(Day day)
        {
            return new DailyTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = false,
                Mark = "Mark1",
                Status = "Status1",
                Type = "Type2",
                Title = "Title1",
                Start = "00:00",
                End = "00:50",
                DayId = day.Id,
            };
        }

        private GlobalTask GetGlobalTask(int days)
        {
            return new GlobalTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = false,
                Mark = "Mark1",
                Status = "Status1",
                Type = "Type2",
                Title = "Title1",
                DeadLine = DateTime.Now.AddDays(days),
            };
        }

        private GlobalTask GetGlobalPlan(int days)
        {
            return new GlobalTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = true,
                Mark = "Mark1",
                Status = "Status1",
                Type = "Type2",
                Title = "Title1",
                DeadLine = DateTime.Now.AddDays(days),
            };
        }

        private DailyTask GetDailyPlan(Day day)
        {
            return new DailyTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = true,
                Mark = "Mark1",
                Status = "Status1",
                Type = "Type1",
                Title = "Title1",
                Start = "00:00",
                End = "00:50",
                DayId = day.Id,
            };
        }
    }
}
