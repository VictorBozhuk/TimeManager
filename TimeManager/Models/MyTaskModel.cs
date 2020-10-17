using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.ViewModels;

namespace TimeManager.Models
{
    public class MyTaskModel : BaseViewModel
    {
        private string name;
        private string mark;
        private string type;
        private string date;
        private string start;
        private string end;


        public MyTaskModel()
        {

        }

        public MyTaskModel(MyTask task, int index = 1)
        {
            Name = task.Name;
            Type = task.Type;
            Mark = task.Mark;
            Date = task.Date.ToString();
            Start = task.Start.ToString();
            End = task.End.ToString();
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged(nameof(Mark));
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged(nameof(Start));
            }
        }

        public string End
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged(nameof(End));
            }
        }
    }
}
