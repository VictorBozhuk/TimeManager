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
        private string selectedTemplateTab;
        private string selectedTaskTab;
        public DateTime pickeredDate;
        public bool DailyPlansVisible { get; set; } = true;
        public string TypeOfDailyTasks { get; set; }
        public DailyTaskModel SelectedDailyTask { get; set; }
        public DayModel Day { get; set; }
        public ObservableCollection<DailyTaskModel> DailyTasks { get; set; }
        public Page CreateEditDailyTaskOrGetTemplatesFrame { get; set; }
        public CreateEditDailyTaskViewModel CreateEditDailyTaskVM { get; set; }
        public GlobalTemplatesViewModel GlobalTemplatesVM { get; set; }
        public List<string> TemplateTabs { get; set; } = new List<string>() { Texts.CreateForm, Texts.Templates, };
        public List<string> TaskTabs { get; set; } = new List<string>() { Texts.Plans, Texts.Tasks, };
        public CreateEditDayViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage, DayModel day = null, bool isPlans = true, DailyTaskModel task = null) 
            : base(main, dayStorage, dailyTaskStorage)
        {
            CreateEditDailyTaskOrGetTemplatesFrame = new CreateEditDailyTask(main);
            CreateEditDailyTaskVM = new CreateEditDailyTaskViewModel(main, dayStorage, dailyTaskStorage, globalTaskStorage);
            GlobalTemplatesVM = new GlobalTemplatesViewModel(main, dayStorage, dailyTaskStorage, globalTaskStorage);
            ShowDailyPlansCommand = new RelayCommand(ShowDailyPlans);
            ShowDailyTasksCommand = new RelayCommand(ShowDailyDoneTasks);
            DeleteDailyTaskCommand = new RelayCommand(DeleteDailyTask);
            EditDailyTaskCommand = new RelayCommand(EditDailyTask);
            SelectedTemplateTab = TemplateTabs.FirstOrDefault();
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
            SelectedTaskTab = TaskTabs.FirstOrDefault();
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

        public string SelectedTemplateTab
        {
            get { return selectedTemplateTab; }
            set
            {
                if(selectedTemplateTab != value && value == Texts.CreateForm)
                {
                    CreateEditDailyTaskOrGetTemplatesFrame = new CreateEditDailyTask(_main);
                }
                else if(selectedTemplateTab != value && value == Texts.Templates)
                {
                    CreateEditDailyTaskOrGetTemplatesFrame = new GlobalTemplates(_main);
                }

                selectedTemplateTab = value;
            }
        }

        public string SelectedTaskTab
        {
            get { return selectedTaskTab; }
            set
            {
                if (selectedTaskTab != value && value == Texts.Plans)
                {
                    ShowDailyPlans();
                }
                else if (selectedTaskTab != value && value == Texts.Tasks)
                {
                    ShowDailyDoneTasks();
                }

                selectedTaskTab = value;
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
                ShowDailyPlans();
            }
            else
            {
                ShowDailyDoneTasks();
            }
        }

        private void DeleteDailyTask()
        {
            _dailyTaskStorage.Delete(SelectedDailyTask.Id);
            if(_dayStorage.GetAllDays().FirstOrDefault(x => x.Date == SelectedDailyTask.Day.Date).DailyTasks.Count == 0)
            {
                _dayStorage.Delete(SelectedDailyTask.Day.Id);
            }
            ShowDailyTasks();
        }

        private void EditDailyTask()
        {
            CreateEditDailyTaskVM.PrepareEditDailyTask(SelectedDailyTask);
        }

        private void ShowDailyDoneTasks()
        {
            TypeOfDailyTasks = Texts.Tasks;
            DailyPlansVisible = false;
            var newMyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date.ToShortDateString() == Day.Date.ToShortDateString() && x.IsPlan == false).ToList();
            DailyTasks = new ObservableCollection<DailyTaskModel>(newMyTasks.Select(x => new DailyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }

        private void ShowDailyPlans()
        {
            TypeOfDailyTasks = Texts.Plans;
            DailyPlansVisible = true;
            var newMyTasks = _dailyTaskStorage.GetAllDailyTasks().Where(x => x.Day.Date.ToShortDateString() == Day.Date.ToShortDateString() && x.IsPlan == true).ToList();
            DailyTasks = new ObservableCollection<DailyTaskModel>(newMyTasks.Select(x => new DailyTaskModel(x, newMyTasks.IndexOf(x))).ToList());
        }
    }
}
