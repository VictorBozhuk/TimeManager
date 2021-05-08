using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.ViewModels;

namespace TimeManager.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DailyTaskModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Mark { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Interval { get; set; }
        public Day Day { get; set; }

        public DailyTaskModel() { }

        public DailyTaskModel(DailyTask task)
        {
            SetValues(task);
        }

        public DailyTaskModel(DailyTask task, int index = 1)
        {
            SetValues(task);
        }

        private void SetValues(DailyTask task)
        {
            Id = task.Id.ToString();
            Title = task.Title;
            Type = task.Type;
            Status = task.Status;
            Mark = task.Mark;
            Start = task.Start;
            End = task.End;
            Day = task.Day;
            Interval = $"{Start} - {End}";
        }
    }
}
