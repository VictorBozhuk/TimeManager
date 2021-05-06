using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DayModel
    {
        public Guid Id { get; set; }
        private DateTime date;

        public DayModel(DateTime date)
        {
            Date = date;
        }
        public DayModel(Day day)
        {
            Id = day.Id;
            Date = day.Date;
            Plans = day.Tasks.Where(x => x.IsPlan).Select(x => new MyTaskModel(x)).ToList();
            Tasks = day.Tasks.Where(x => !x.IsPlan).Select(x => new MyTaskModel(x)).ToList();
        }
        public string DateShortString { get; set; }
        public DateTime Date
        { 
            get { return date; }
            set
            {
                date = value;
                DateShortString = value.ToShortDateString();
            }
        }
        public List<MyTaskModel> Plans { get; set; }
        public List<MyTaskModel> Tasks { get; set; }

        public override string ToString()
        {
            return DateShortString;
        }
    }
}
