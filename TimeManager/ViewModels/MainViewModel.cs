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
        private IMyTaskStorage _taskStorage;
        public MainPageViewModel MainPageVM { get; set; }
        public CreateEditDayViewModel CreateEditDayVM { get; set; }

        public Page MainFrame { get; set; }
        public MainViewModel()
        {
            _dbContext = new TimeManagerDbContext();
            _dayStorage = new DayStorage(_dbContext);
            _taskStorage = new MyTaskStorage(_dbContext);
            Initializer();
            MainFrame = new MainPage(this);
            MainPageVM = new MainPageViewModel(this, _dayStorage, _taskStorage);
            CreateDayCommand = new RelayCommand(GoToCreateDay);
            GoToMainPageCommand = new RelayCommand(GoToMainPage);
        }

        public RelayCommand GoToMainPageCommand { get; set; }
        public RelayCommand CreateEditDayCommand { get; set; }
        public RelayCommand CreateDayCommand { get; set; }


        private void GoToCreateDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _taskStorage);
        }

        private void GoToEditDay()
        {
            MainFrame = new CreateEditDay(this);
            CreateEditDayVM = new CreateEditDayViewModel(this, _dayStorage, _taskStorage);
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

                var plan1 = GetPlan(day1);
                var plan2 = GetPlan(day1);
                var plan3 = GetPlan(day1);
                var plan4 = GetPlan(day2);
                var plan5 = GetPlan(day2);
                var plan6 = GetPlan(day2);
                var plan7 = GetPlan(day3);
                var plan8 = GetPlan(day3);
                var plan9 = GetPlan(day3);

                var task1 = GetTask(day1);
                var task2 = GetTask(day1);
                var task3 = GetTask(day1);
                var task4 = GetTask(day2);
                var task5 = GetTask(day2);
                var task6 = GetTask(day2);



                _dbContext.Days.Add(day1);
                _dbContext.Days.Add(day2);
                _dbContext.Days.Add(day3);

                _dbContext.MyTasks.Add(plan1);
                _dbContext.MyTasks.Add(plan2);
                _dbContext.MyTasks.Add(plan3);
                _dbContext.MyTasks.Add(plan4);
                _dbContext.MyTasks.Add(plan5);
                _dbContext.MyTasks.Add(plan6);
                _dbContext.MyTasks.Add(plan7);
                _dbContext.MyTasks.Add(plan8);
                _dbContext.MyTasks.Add(plan9);
                _dbContext.MyTasks.Add(task1);
                _dbContext.MyTasks.Add(task2);
                _dbContext.MyTasks.Add(task3);
                _dbContext.MyTasks.Add(task4);
                _dbContext.MyTasks.Add(task5);
                _dbContext.MyTasks.Add(task6);
                _dbContext.SaveChanges();
            }

        }
        private MyTask GetTask(Day day)
        {
            return new MyTask()
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

        private MyTask GetPlan(Day day)
        {
            return new MyTask()
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
