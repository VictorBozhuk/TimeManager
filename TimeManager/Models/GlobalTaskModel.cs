using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeManager.Abstracts;
using TimeManager.Models.Abstracts;

namespace TimeManager.Models
{
    [AddINotifyPropertyChangedInterface]
    public class GlobalTaskModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public bool IsPlan { get; set; }
        public string DeadLine { get; set; }
        public string DeadLineShortDate { get; set; }
        public string DeadLineLongDate { get; set; }
        public string DeadLineTime { get; set; }
        public string DayOfWeek { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string TimeSpent { get; set; }
        public string TimeSpentByProcents { get; set; }
        public List<DailyTaskModel> DailyTasks { get; set; }
        public Brush RowColor { get; set; }
        public Brush StatusColor { get; set; }
        public double? HeightDescriptionBlock { get; set; } = null;

        public GlobalTaskModel() { }

        public GlobalTaskModel(GlobalTask task)
        {
            SetValues(task);
        }

        public GlobalTaskModel(GlobalTask task, int index = 1)
        {
            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
            SetValues(task);
        }

        private void SetValues(GlobalTask task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Type = task.Type;
            Status = task.Status;
            IsPlan = task.IsPlan;
            DeadLineShortDate = task.DeadLine.ToShortDateString();
            DeadLineLongDate = task.DeadLine.ToLongDateString();
            DeadLineTime = task.DeadLine.ToShortTimeString();
            DayOfWeek = DayParser.TranslateDayOfWeek(task.DeadLine.DayOfWeek.ToString());
            DeadLine = $"{DeadLineShortDate} {DeadLineTime} {DayOfWeek}";
            DailyTasks = task.DailyTasks.Select(x => new DailyTaskModel(x)).ToList();

            if(Status == Statuses.Done)
            {
                StatusColor = Statuses.ColorDone;
            }
            else if (Status == Statuses.InProgress)
            {
                StatusColor = Statuses.ColorInProgress;
            }
            else if (Status == Statuses.NotDone)
            {
                StatusColor = Statuses.ColorNotDone;
            }

            if (string.IsNullOrEmpty(Description))
            {
                HeightDescriptionBlock = 0;
            }
        }
    }
}
