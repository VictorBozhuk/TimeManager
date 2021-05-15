using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Mark { get; set; }
        public bool IsPlan { get; set; }
        public float TimeSpent { get; set; }
        public string DeadLine { get; set; }
        public string DeadLineDate { get; set; }
        public string DeadLineTime { get; set; }

        public GlobalTaskModel() { }

        public GlobalTaskModel(GlobalTask task)
        {
            SetValues(task);
        }

        public GlobalTaskModel(GlobalTask task, int index = 1)
        {
            SetValues(task);
        }

        private void SetValues(GlobalTask task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Type = task.Type;
            Status = task.Status;
            Mark = task.Mark;
            IsPlan = task.IsPlan;
            TimeSpent = task.TimeSpent;
            DeadLineDate = task.DeadLine.ToShortDateString();
            DeadLineTime = task.DeadLine.ToShortTimeString();
            DeadLine = $"{DeadLineDate} {DeadLineTime}";
        }
    }
}
