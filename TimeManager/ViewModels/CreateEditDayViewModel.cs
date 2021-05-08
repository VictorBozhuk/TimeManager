using GalaSoft.MvvmLight.CommandWpf;
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
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditDayViewModel : BaseViewModel
    {
        private readonly IDailyTaskStorage _dailyTaskStorage;
        private readonly IDayStorage _dayStorage;
        public DateTime pickeredDate;
        public bool DailyPlansVisible { get; set; } = true;
        public string TypeOfDailyTasks { get; set; }
        public DailyTaskModel SelectedDailyTask { get; set; }
        public DayModel Day { get; set; }

        public ObservableCollection<DailyTaskModel> DailyTasks { get; set; }
        public Page CreateEditDailyTaskFrame { get; set; }
        public CreateEditDailyTaskViewModel CreateEditDailyTaskVM { get; set; }
        public CreateEditDayViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dayliTaskStorage, DayModel day = null, bool isPlans = true, DailyTaskModel task = null)
        {
            _dailyTaskStorage = dayliTaskStorage;
            _dayStorage = dayStorage;
            CreateEditDailyTaskFrame = new CreateEditDailyTask(main);
            CreateEditDailyTaskVM = new CreateEditDailyTaskViewModel(main, dayStorage, dayliTaskStorage);
            ShowDailyPlansCommand = new RelayCommand(() => ShowDailyTasks(Texts.Plans, true));
            ShowDailyTasksCommand = new RelayCommand(() => ShowDailyTasks(Texts.Tasks, false));
            DeleteDailyTaskCommand = new RelayCommand(DeleteDailyTask);
            EditDailyTaskCommand = new RelayCommand(EditDailyTask);
            if (day == null)
            {
                var h = dayStorage.GetAllDays();
                var firstDay = h.FirstOrDefault();
                if (firstDay != null && firstDay.Date.ToShortDateString() == DateTime.Now.ToShortDateString() || firstDay.Date > DateTime.Now)
                {
                    Day = new DayModel(firstDay.Date.AddDays(1));
                    PickeredDate = Day.Date;
                }
                else
                {
                    Day = new DayModel(DateTime.Now);
                    PickeredDate = Day.Date;
                }
            }
            else
            {
                Day = day;
                PickeredDate = day.Date;
                DailyPlansVisible = isPlans;
                ShowDailyTasks();
                if(task != null)
                {
                    SelectedDailyTask = DailyTasks.FirstOrDefault(x => x.Id == task.Id);
                    EditDailyTask();
                }
            }

        }

        public RelayCommand ShowDailyPlansCommand { get; set; }
        public RelayCommand ShowDailyTasksCommand { get; set; }
        public RelayCommand DeleteDailyTaskCommand { get; set; }
        public RelayCommand EditDailyTaskCommand { get; set; }

        public DateTime PickeredDate
        {
            get { return pickeredDate; }
            set
            {
                pickeredDate = value;
                Day.Date = value;
                ShowDailyTasks();
            }
        }

        public void RefreshDay()
        {
            var dayFromStorage = _dayStorage.GetAllDays().FirstOrDefault(x => x.Date.ToShortDateString() == Day.DateShortString);
            Day = new DayModel(dayFromStorage);
        }

        public void ShowDailyTasks()
        {
            if (DailyPlansVisible)
            {
                ShowDailyTasks(Texts.Plans, true);
            }
            else
            {
                ShowDailyTasks(Texts.Tasks, false);
            }
        }

        private void DeleteDailyTask()
        {
            _dailyTaskStorage.Delete(SelectedDailyTask.Id);
            if(_dayStorage.GetAllDays().FirstOrDefault(x => x.Date == SelectedDailyTask.Day.Date).DailyTasks.Count == 0)
            {
                _dayStorage.Delete(SelectedDailyTask.Day.Id.ToString());
            }
            ShowDailyTasks();
        }

        private void EditDailyTask()
        {
            CreateEditDailyTaskVM.PrepareEditDailyTask(SelectedDailyTask);
        }

        private void ShowDailyTasks(string typeOfDailyTask, bool isPlan)
        {
            TypeOfDailyTasks = typeOfDailyTask;
            DailyPlansVisible = isPlan;
            var newMyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date.ToShortDateString() == Day.Date.ToShortDateString() && x.IsPlan == isPlan).ToList();
            DailyTasks = new ObservableCollection<DailyTaskModel>(newMyTasks.Select(x => new DailyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }
    }
}
