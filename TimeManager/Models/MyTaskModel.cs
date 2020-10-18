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
    public class MyTaskModel : BaseViewModel
    {
        private string id;
        private string name;
        private string mark;
        private string type;
        private string date;
        private string start;
        private string end;
        private ComboBoxItem selectedMark;
        public ObservableCollection<ComboBoxItem> Marks { get; set; }

        public MyTaskModel(MyTask task)
        {
            SetValues(task);
            Marks = GetMarks();
            if(Mark != null)
            {
                SelectedMark = Marks.FirstOrDefault(x => x.Content.ToString() == Mark);
                if(SelectedMark == null)
                {
                    SelectedMark = Marks.First();
                }
            }
            else
            {
                SelectedMark = Marks.First();
            }
        }

        private void SetValues(MyTask task)
        {
            Id = task.Id.ToString();
            Name = task.Name;
            Type = task.Type;
            Mark = task.Mark;
            Date = task.Date.ToShortDateString();
            Start = task.Start.ToShortTimeString();
            End = task.End.ToShortTimeString();
        }

        public MyTaskModel(MyTask task, int index = 1)
        {
            Id = task.Id.ToString();
            Name = task.Name;
            Type = task.Type;
            Mark = task.Mark;
            Date = task.Date.ToString();
            Start = task.Start.ToString();
            End = task.End.ToString();
        }

        #region getters and setters
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
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

        public ComboBoxItem SelectedMark
        {
            get { return selectedMark; }
            set
            {
                selectedMark = value;
                OnPropertyChanged(nameof(SelectedMark));
            }
        }
        #endregion

        private ObservableCollection<ComboBoxItem> GetMarks()
        {
            return new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem() {Content = "Badly"},
                new ComboBoxItem() {Content = "Satisfactorily"},
                new ComboBoxItem() {Content = "Good"},
                new ComboBoxItem() {Content = "Excellent"},
            };
        }
    }
}
