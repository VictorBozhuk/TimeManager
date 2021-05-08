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
        public MainPageViewModel MainPageVM { get; set; }
        public CreateEditDayViewModel CreateEditDayVM { get; set; }

        public Page MainFrame { get; set; }
        public MainViewModel()
        {
            _dbContext = new TimeManagerDbContext();
            _dayStorage = new DayStorage(_dbContext);
            _dailyTaskStorage = new DailyTaskStorage(_dbContext);
            Initializer();
            MainFrame = new MainPage(this);
            MainPageVM = new MainPageViewModel(this, _dayStorage, _dailyTaskStorage);
            CreateDayCommand = new RelayCommand(GoToCreateDay);
            GoToMainPageCommand = new RelayCommand(GoToMainPage);
        }

        public RelayCommand GoToMainPageCommand { get; set; }
        public RelayCommand CreateEditDayCommand { get; set; }
        public RelayCommand CreateDayCommand { get; set; }


        private void GoToCreateDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _dailyTaskStorage);
        }

        private void GoToEditDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _dailyTaskStorage);
        }

        private void GoToMainPage()
        {
            MainFrame = new MainPage(this);
            MainPageVM.LoadDays();
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
