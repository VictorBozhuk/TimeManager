using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstracts;
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
        public StatisticViewModel StatisticVM { get; set; }

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
            GoToStatisticCommand = new RelayCommand(GoToStatistic);
        }

        public RelayCommand GoToDailyTasksCommand { get; set; }
        public RelayCommand GoToGlobalTasksCommand { get; set; }
        public RelayCommand GoToStatisticCommand { get; set; }
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

        private void GoToStatistic()
        {
            MainFrame = new Statistic(this);
            StatisticVM = new StatisticViewModel(this, _dayStorage, _dailyTaskStorage, _globalTaskStorage);
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
                    Date = DateTime.Now.AddDays(-1),
                    Id = Guid.NewGuid(),
                };
                var day3 = new Day()
                {
                    Date = DateTime.Now.AddDays(-2),
                    Id = Guid.NewGuid(),
                };

                var day31 = new Day()
                {
                    Date = DateTime.Now.AddDays(-3),
                    Id = Guid.NewGuid(),
                };

                var day32 = new Day()
                {
                    Date = DateTime.Now.AddDays(-4),
                    Id = Guid.NewGuid(),
                };
                var day33 = new Day()
                {
                    Date = DateTime.Now.AddDays(-5),
                    Id = Guid.NewGuid(),
                };

                var day21 = new Day()
                {
                    Date = DateTime.Now.AddDays(-6),
                    Id = Guid.NewGuid(),
                };
                var day22 = new Day()
                {
                    Date = DateTime.Now.AddDays(-7),
                    Id = Guid.NewGuid(),
                };
                var day23 = new Day()
                {
                    Date = DateTime.Now.AddDays(-8),
                    Id = Guid.NewGuid(),
                };

                var day11 = new Day()
                {
                    Date = DateTime.Now.AddDays(-9),
                    Id = Guid.NewGuid(),
                };
                var day12 = new Day()
                {
                    Date = DateTime.Now.AddDays(-10),
                    Id = Guid.NewGuid(),
                };
                var day13 = new Day()
                {
                    Date = DateTime.Now.AddDays(-11),
                    Id = Guid.NewGuid(),
                };

                AddDays(new List<Day>() { day1, day2, day3, day31, day32, day33, day21, day22, day23, day11, day12, day13, });

                AddPlans(day1, day2, day3);
                AddPlans(day11, day12, day13);
                AddPlans(day21, day22, day23);
                AddPlans(day31, day32, day33);

                AddTasks(day1, day2, day3);
                AddTasks(day11, day12, day13);
                AddTasks(day21, day22, day23);
                AddTasks(day31, day32, day33);

                AddGlobalPlan();
                AddGlobalTask();

                _dbContext.SaveChanges();
            }

        }

        private void AddDays(List<Day> days)
        {
            foreach (var item in days)
            {
                _dbContext.Days.Add(item);

            }
        }

        private void AddPlans(Day day1, Day day2, Day day3)
        {
            var plan1 = GetDailyPlan(day1, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan2 = GetDailyPlan(day1, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan3 = GetDailyPlan(day1, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan31 = GetDailyPlan(day1, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan32 = GetDailyPlan(day1, "Перегляд завдань, аналіз, виставлення пріорітетів", "Робота", "10:00", "10:10");
            var plan33 = GetDailyPlan(day1, "Завдання 1,2...", "Робота", "10:10", "13:10");
            var plan34 = GetDailyPlan(day1, "Обід, перегляд ютубу", "Прийом їжі", "13:10", "13:40");
            var plan35 = GetDailyPlan(day1, "Завдання 3,4...", "Робота", "13:40", "18:30");
            var plan36 = GetDailyPlan(day1, "Байдики б'ю", "Відпочинок", "18:30", "19:00");
            var plan36_ = GetDailyPlan(day1, "Зал", "Тренування", "19:00", "20:20");
            var plan37 = GetDailyPlan(day1, "Вечеря", "Прийом їжі", "20:20", "20:50");
            var plan38 = GetDailyPlan(day1, "Душ", "Буденність", "20:50", "21:05");
            var plan39 = GetDailyPlan(day1, "Перегляд фільму англійською", "Розвиток", "21:05", "22:40");
            var plan39_ = GetDailyPlan(day1, "Lab 8 (Системний аналіз)", "Універ", "22:40", "23:40");
            var plan391 = GetDailyPlan(day1, "Другорядні задачі з низькою пріорітетністю", "Інше", "23:40", "01:00");
            var plan392 = GetDailyPlan(day1, "Сон", "Сон", "01:00", "09:00");

            var plan4 = GetDailyPlan(day2, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan5 = GetDailyPlan(day2, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan6 = GetDailyPlan(day2, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan61 = GetDailyPlan(day2, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan62 = GetDailyPlan(day2, "Написати зберігання та відтворення історії тренсверів музики", "Робота", "10:00", "13:00");
            var plan63 = GetDailyPlan(day2, "Тестування та виправлення можливих багів", "Робота", "13:00", "14:00");
            var plan64 = GetDailyPlan(day2, "Обід, перегляд ютубу", "Прийом їжі", "14:00", "14:30");
            var plan65 = GetDailyPlan(day2, "Рев'ю коду, запис відео пояснення", "Робота", "14:30", "15:30");
            var plan66 = GetDailyPlan(day2, "Вирішення проблеми від користувача yanrich@gmail.com", "Робота", "15:30", "17:00");
            var plan67 = GetDailyPlan(day2, "Вирішення не пріорітетних задачок, до години часу", "Робота", "17:00", "18:00");
            var plan68 = GetDailyPlan(day2, "Байдики б'ю", "Відпочинок", "18:00", "18:30");
            var plan69 = GetDailyPlan(day2, "Зал", "Тренування", "18:30", "19:50");
            var plan611 = GetDailyPlan(day2, "Вечеря", "Прийом їжі", "19:50", "20:20");
            var plan622 = GetDailyPlan(day2, "Душ", "Буденність", "20:20", "20:35");
            var plan633 = GetDailyPlan(day2, "Написати інтерпретатор", "Універ", "20:35", "22:35");
            var plan644 = GetDailyPlan(day2, "Другорядні задачі з низькою пріорітетністю", "Інше", "22:35", "01:00");
            var plan655 = GetDailyPlan(day2, "Сон", "Сон", "01:00", "09:00");

            var plan7 = GetDailyPlan(day3, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan8 = GetDailyPlan(day3, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan9 = GetDailyPlan(day3, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan91 = GetDailyPlan(day3, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan92 = GetDailyPlan(day3, "Робота над дизайном", "Робота", "10:00", "12:00");
            var plan93 = GetDailyPlan(day3, "Мітинг", "Робота", "12:00", "13:00");
            var plan94 = GetDailyPlan(day3, "Робота над дизайном", "Робота", "13:00", "14:00");
            var plan95 = GetDailyPlan(day3, "Обід, перегляд ютубу", "Прийом їжі", "14:00", "14:30");
            var plan96 = GetDailyPlan(day3, "Робота над дизайном, пошук іконок або самостійно створити в фотошопі", "Робота", "14:30", "16:30");
            var plan97 = GetDailyPlan(day3, "Код рев'ю", "Робота", "16:30", "17:30");
            var plan98 = GetDailyPlan(day3, "Вирішення не пріорітетних задачок, до години часу", "Робота", "17:30", "18:30");
            var plan99 = GetDailyPlan(day3, "Байдики б'ю", "Відпочинок", "18:30", "19:00");
            var plan911 = GetDailyPlan(day3, "Зал", "Тренування", "19:00", "20:20");
            var plan922 = GetDailyPlan(day3, "Вечеря", "Прийом їжі", "20:20", "20:50");
            var plan933 = GetDailyPlan(day3, "Душ", "Буденність", "20:50", "21:05");
            var plan944 = GetDailyPlan(day3, "Презентації та Лабораторні до Соколовського", "Універ", "21:05", "00:20");
            var plan955 = GetDailyPlan(day3, "Другорядні задачі з низькою пріорітетністю", "Інше", "00:20", "01:00");
            var plan966 = GetDailyPlan(day3, "Сон", "Сон", "01:00", "09:00");

            #region 111111111111111111
            _dbContext.DailyTasks.Add(plan1);
            _dbContext.DailyTasks.Add(plan2);
            _dbContext.DailyTasks.Add(plan3);
            _dbContext.DailyTasks.Add(plan4);
            _dbContext.DailyTasks.Add(plan5);
            _dbContext.DailyTasks.Add(plan6);
            _dbContext.DailyTasks.Add(plan7);
            _dbContext.DailyTasks.Add(plan8);
            _dbContext.DailyTasks.Add(plan9);
            _dbContext.DailyTasks.Add(plan31);
            _dbContext.DailyTasks.Add(plan32);
            _dbContext.DailyTasks.Add(plan33);
            _dbContext.DailyTasks.Add(plan34);
            _dbContext.DailyTasks.Add(plan35);
            _dbContext.DailyTasks.Add(plan36);
            _dbContext.DailyTasks.Add(plan36_);
            _dbContext.DailyTasks.Add(plan37);
            _dbContext.DailyTasks.Add(plan38);
            _dbContext.DailyTasks.Add(plan39);
            _dbContext.DailyTasks.Add(plan39_);
            _dbContext.DailyTasks.Add(plan391);
            _dbContext.DailyTasks.Add(plan392);
            #endregion

            #region 2222222222222222222
            _dbContext.DailyTasks.Add(plan61);
            _dbContext.DailyTasks.Add(plan62);
            _dbContext.DailyTasks.Add(plan63);
            _dbContext.DailyTasks.Add(plan64);
            _dbContext.DailyTasks.Add(plan65);
            _dbContext.DailyTasks.Add(plan66);
            _dbContext.DailyTasks.Add(plan67);
            _dbContext.DailyTasks.Add(plan68);
            _dbContext.DailyTasks.Add(plan69);
            _dbContext.DailyTasks.Add(plan611);
            _dbContext.DailyTasks.Add(plan622);
            _dbContext.DailyTasks.Add(plan633);
            _dbContext.DailyTasks.Add(plan644);
            _dbContext.DailyTasks.Add(plan655);
            #endregion

            _dbContext.DailyTasks.Add(plan91);
            _dbContext.DailyTasks.Add(plan92);
            _dbContext.DailyTasks.Add(plan93);
            _dbContext.DailyTasks.Add(plan94);
            _dbContext.DailyTasks.Add(plan95);
            _dbContext.DailyTasks.Add(plan96);
            _dbContext.DailyTasks.Add(plan97);
            _dbContext.DailyTasks.Add(plan98);
            _dbContext.DailyTasks.Add(plan99);
            _dbContext.DailyTasks.Add(plan911);
            _dbContext.DailyTasks.Add(plan922);
            _dbContext.DailyTasks.Add(plan933);
            _dbContext.DailyTasks.Add(plan944);
            _dbContext.DailyTasks.Add(plan955);
            _dbContext.DailyTasks.Add(plan966);

        }

        private void AddTasks(Day day1, Day day2, Day day3)
        {
            var plan1 = GetDailyTask(day1, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan2 = GetDailyTask(day1, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan3 = GetDailyTask(day1, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan31 = GetDailyTask(day1, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan32 = GetDailyTask(day1, "Перегляд завдань, аналіз, виставлення пріорітетів", "Робота", "10:00", "10:10");
            var plan33 = GetDailyTask(day1, "Написати сторінку Contact, Support", "Робота", "10:10", "13:10");
            var plan34 = GetDailyTask(day1, "Обід, перегляд ютубу", "Прийом їжі", "13:10", "13:40");
            var plan35 = GetDailyTask(day1, "Тестування Contact Support та виправлення багів", "Робота", "13:40", "14:30");
            var plan35_ = GetDailyTask(day1, "Вирішення проблеми користувача rra.jjaa@gmail.com", "Робота", "14:30", "18:30");
            var plan36 = GetDailyTask(day1, "Байдики б'ю", "Відпочинок", "18:30", "19:00");
            var plan36_ = GetDailyTask(day1, "Зал", "Тренування", "19:00", "20:20");
            var plan37 = GetDailyTask(day1, "Вечеря", "Прийом їжі", "20:20", "20:50");
            var plan38 = GetDailyTask(day1, "Душ", "Буденність", "20:50", "21:05");
            var plan39 = GetDailyTask(day1, "Перегляд фільму англійською", "Розвиток", "21:05", "22:40");
            var plan39_ = GetDailyTask(day1, "Lab 8 (Системний аналіз)", "Універ", "22:40", "23:40");
            var plan391 = GetDailyTask(day1, "Другорядні задачі з низькою пріорітетністю", "Інше", "23:40", "01:00");
            var plan392 = GetDailyTask(day1, "Сон", "Сон", "01:00", "09:00");

            var plan4 = GetDailyTask(day2, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan5 = GetDailyTask(day2, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan6 = GetDailyTask(day2, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan61 = GetDailyTask(day2, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan62 = GetDailyTask(day2, "Написати зберігання та відтворення історії тренсверів музики", "Робота", "10:00", "13:00");
            var plan63 = GetDailyTask(day2, "Тестування та виправлення можливих багів", "Робота", "13:00", "14:00");
            var plan64 = GetDailyTask(day2, "Обід, перегляд ютубу", "Прийом їжі", "14:00", "14:30");
            var plan65 = GetDailyTask(day2, "Рев'ю коду, запис відео пояснення", "Робота", "14:30", "15:30");
            var plan66 = GetDailyTask(day2, "Вирішення проблеми від користувача yanrich@gmail.com", "Робота", "15:30", "18:00");
            var plan68 = GetDailyTask(day2, "Байдики б'ю", "Відпочинок", "18:00", "18:30");
            var plan69 = GetDailyTask(day2, "Зал", "Тренування", "18:30", "19:50");
            var plan611 = GetDailyTask(day2, "Вечеря", "Прийом їжі", "19:50", "20:20");
            var plan622 = GetDailyTask(day2, "Душ", "Буденність", "20:20", "20:35");
            var plan633 = GetDailyTask(day2, "Написати інтерпретатор", "Універ", "20:35", "22:35");
            var plan644 = GetDailyTask(day2, "Другорядні задачі з низькою пріорітетністю", "Інше", "22:35", "01:00");
            var plan655 = GetDailyTask(day2, "Сон", "Сон", "01:00", "09:00");

            var plan7 = GetDailyTask(day3, "Прокинутись", "Буденність", "09:00", "09:15");
            var plan8 = GetDailyTask(day3, "Сніданок", "Прийом їжі", "09:15", "09:45");
            var plan9 = GetDailyTask(day3, "Перегляд соц. мереж", "Соц. мережі", "09:45", "09:50");
            var plan91 = GetDailyTask(day3, "Планування дня, перегляд завдань", "Планування", "09:50", "10:00");
            var plan92 = GetDailyTask(day3, "Робота над дизайном", "Робота", "10:00", "12:00");
            var plan93 = GetDailyTask(day3, "Мітинг", "Робота", "12:00", "13:00");
            var plan94 = GetDailyTask(day3, "Робота над дизайном", "Робота", "13:00", "14:00");
            var plan95 = GetDailyTask(day3, "Обід, перегляд ютубу", "Прийом їжі", "14:00", "14:30");
            var plan96 = GetDailyTask(day3, "Робота над дизайном, пошук іконок або самостійно створити в фотошопі", "Робота", "14:30", "16:30");
            var plan97 = GetDailyTask(day3, "Код рев'ю", "Робота", "16:30", "17:30");
            var plan98 = GetDailyTask(day3, "Вирішення не пріорітетних задачок, до години часу", "Робота", "17:30", "18:30");
            var plan99 = GetDailyTask(day3, "Байдики б'ю", "Відпочинок", "18:30", "19:00");
            var plan911 = GetDailyTask(day3, "Зал", "Тренування", "19:00", "20:20");
            var plan922 = GetDailyTask(day3, "Вечеря", "Прийом їжі", "20:20", "20:50");
            var plan933 = GetDailyTask(day3, "Душ", "Буденність", "20:50", "21:05");
            var plan944 = GetDailyTask(day3, "Презентації та Лабораторні до Соколовського", "Універ", "21:05", "00:20");
            var plan955 = GetDailyTask(day3, "Другорядні задачі з низькою пріорітетністю", "Інше", "00:20", "01:00");
            var plan966 = GetDailyTask(day3, "Сон", "Сон", "01:00", "09:00");


            #region 111111111111111111
            _dbContext.DailyTasks.Add(plan1);
            _dbContext.DailyTasks.Add(plan2);
            _dbContext.DailyTasks.Add(plan3);
            _dbContext.DailyTasks.Add(plan4);
            _dbContext.DailyTasks.Add(plan5);
            _dbContext.DailyTasks.Add(plan6);
            _dbContext.DailyTasks.Add(plan7);
            _dbContext.DailyTasks.Add(plan8);
            _dbContext.DailyTasks.Add(plan9);
            _dbContext.DailyTasks.Add(plan31);
            _dbContext.DailyTasks.Add(plan32);
            _dbContext.DailyTasks.Add(plan33);
            _dbContext.DailyTasks.Add(plan34);
            _dbContext.DailyTasks.Add(plan35);
            _dbContext.DailyTasks.Add(plan35_);
            _dbContext.DailyTasks.Add(plan36);
            _dbContext.DailyTasks.Add(plan36_);
            _dbContext.DailyTasks.Add(plan37);
            _dbContext.DailyTasks.Add(plan38);
            _dbContext.DailyTasks.Add(plan39);
            _dbContext.DailyTasks.Add(plan39_);
            _dbContext.DailyTasks.Add(plan391);
            _dbContext.DailyTasks.Add(plan392);
            #endregion

            #region 2222222222222222222
            _dbContext.DailyTasks.Add(plan61);
            _dbContext.DailyTasks.Add(plan62);
            _dbContext.DailyTasks.Add(plan63);
            _dbContext.DailyTasks.Add(plan64);
            _dbContext.DailyTasks.Add(plan65);
            _dbContext.DailyTasks.Add(plan66);
            _dbContext.DailyTasks.Add(plan68);
            _dbContext.DailyTasks.Add(plan69);
            _dbContext.DailyTasks.Add(plan611);
            _dbContext.DailyTasks.Add(plan622);
            _dbContext.DailyTasks.Add(plan633);
            _dbContext.DailyTasks.Add(plan644);
            _dbContext.DailyTasks.Add(plan655);
            #endregion

            _dbContext.DailyTasks.Add(plan91);
            _dbContext.DailyTasks.Add(plan92);
            _dbContext.DailyTasks.Add(plan93);
            _dbContext.DailyTasks.Add(plan94);
            _dbContext.DailyTasks.Add(plan95);
            _dbContext.DailyTasks.Add(plan96);
            _dbContext.DailyTasks.Add(plan97);
            _dbContext.DailyTasks.Add(plan98);
            _dbContext.DailyTasks.Add(plan99);
            _dbContext.DailyTasks.Add(plan911);
            _dbContext.DailyTasks.Add(plan922);
            _dbContext.DailyTasks.Add(plan933);
            _dbContext.DailyTasks.Add(plan944);
            _dbContext.DailyTasks.Add(plan955);
            _dbContext.DailyTasks.Add(plan966);
        }

        private void AddGlobalPlan()
        {
            var gTask1 = GetGlobalPlan(2, "Презентації та лабораторні Соколовський", "Універ");
            var gTask11 = GetGlobalPlan(2, "Lаb 8 , Нечітке Моделювання, Пелюшкевич", "Універ");
            var gTask2 = GetGlobalPlan(3, "Інтерпретатор ( Операційні системи )", "Універ");
            var gTask3 = GetGlobalPlan(40, "ЄВІ", "Універ");
            var gTask31 = GetGlobalPlan(15, "Державний іспит", "Універ");
            var gTask4 = GetGlobalPlan(70, "Вступні іспити (підготовка)", "Універ");
            var gTask41 = GetGlobalPlan(100, "Англійський", "Розвиток");
            _dbContext.GlobalTasks.Add(gTask1);
            _dbContext.GlobalTasks.Add(gTask11);
            _dbContext.GlobalTasks.Add(gTask2);
            _dbContext.GlobalTasks.Add(gTask3);
            _dbContext.GlobalTasks.Add(gTask31);
            _dbContext.GlobalTasks.Add(gTask4);
            _dbContext.GlobalTasks.Add(gTask41);

        }

        private void AddGlobalTask()
        {
            var gTask1 = GetGlobalTask(-1, "Курсова", "Універ");
            var gTask11 = GetGlobalTask(-4, "Graph project , Черняхівський", "Універ");
            var gTask2 = GetGlobalTask(-7, "Lab 4, Пелюшкевич", "Універ");
            var gTask21 = GetGlobalTask(-13, "Lab 3, Пелюшкевич", "Універ");
            var gTask3 = GetGlobalTask(-12, "Написати прогу, Козій", "Універ");
            var gTask31 = GetGlobalTask(-15, "Записати відео туторіал по фідлеру", "Робота");
            var gTask4 = GetGlobalTask(-30, "Перевстановити вінду", "Інше");

            _dbContext.GlobalTasks.Add(gTask1);
            _dbContext.GlobalTasks.Add(gTask11);
            _dbContext.GlobalTasks.Add(gTask2);
            _dbContext.GlobalTasks.Add(gTask3);
            _dbContext.GlobalTasks.Add(gTask31);
            _dbContext.GlobalTasks.Add(gTask4);
            _dbContext.GlobalTasks.Add(gTask21);
        }

        private DailyTask GetDailyTask(Day day, string title, string type, string start, string end)
        {
            return new DailyTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = false,
                Status = Statuses.InProgress,
                Type = type,
                Title = title,
                Start = start,
                End = end,
                DayId = day.Id,
            };
        }

        private GlobalTask GetGlobalTask(int days, string title, string type)
        {
            return new GlobalTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = false,
                Status = Statuses.Done,
                Title = title,
                Type = type,
                DeadLine = DateTime.Now.AddDays(days),
            };
        }

        private GlobalTask GetGlobalPlan(int days, string title, string type)
        {
            return new GlobalTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = true,
                Status = Statuses.InProgress,
                Title = title,
                Type = type,
                DeadLine = DateTime.Now.AddDays(days),
            };
        }

        private DailyTask GetDailyPlan(Day day, string title, string type, string start, string end)
        {
            return new DailyTask()
            {
                Id = Guid.NewGuid(),
                IsPlan = true,
                Status = Statuses.InProgress,
                Type = type,
                Title = title,
                Start = start,
                End = end,
                DayId = day.Id,
            };
        }
    }
}
