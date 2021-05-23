using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TimeManager.Abstract;
using TimeManager.Abstracts;
using TimeManager.Models.Abstracts;
using TimeManager.ViewModels;

namespace TimeManager.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DailyTaskModel : BaseModel
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Interval { get; set; }
        public Day Day { get; set; }
        public Guid? GlobalTaskId { get; set; }

        public DailyTaskModel() { }

        public DailyTaskModel(DailyTask task)
        {
            SetValues(task);
        }

        public DailyTaskModel(GlobalTaskModel task)
        {
            Title = task.Title;
            Description = task.Description;
            Type = task.Type;
            GlobalTaskId = task.Id;
        }

        public DailyTaskModel(DailyTask task, int index = 1) : base(index)
        {
            SetValues(task);
        }

        private void SetValues(DailyTask task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Type = task.Type;
            Status = task.Status;
            Start = task.Start;
            End = task.End;
            Day = task.Day;
            Interval = $"{Start} - {End}";

            SetStatusColor();

            if (string.IsNullOrEmpty(Description))
            {
                HeightDescriptionBlock = 0;
            }
        }
    }
}
