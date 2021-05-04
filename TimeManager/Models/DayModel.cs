using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class DayModel
    {
        //public string Id { get; set; }
        public string DateShortString { get; set; }
        public DateTime Date { get; set; }
        public List<MyTaskModel> Plans { get; set; }
        public List<MyTaskModel> Tasks { get; set; }
        public DayModel(Day day)
        {
            //Id = day.Id.ToString();
            Date = day.Date;
            DateShortString = day.Date.ToShortDateString();
            Plans = day.Tasks.Where(x => x.IsPlan).Select(x => new MyTaskModel(x)).ToList();
            Tasks = day.Tasks.Where(x => !x.IsPlan).Select(x => new MyTaskModel(x)).ToList();
        }
    }
}
