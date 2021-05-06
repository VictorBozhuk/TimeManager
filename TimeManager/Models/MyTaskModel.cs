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
    public class MyTaskModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Mark { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Interval { get; set; }
        public ComboBoxItem SelectedStatus { get; set; }
        public ComboBoxItem SelectedMark { get; set; }
        public ObservableCollection<ComboBoxItem> Statuses { get; set; }
        public ObservableCollection<ComboBoxItem> Marks { get; set; }

        public MyTaskModel() { }

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

        public MyTaskModel(MyTask task, int index = 1)
        {
            SetValues(task);
        }

        private void SetValues(MyTask task)
        {
            Id = task.Id.ToString();
            Title = task.Title;
            Type = task.Type;
            Status = task.Status;
            Mark = task.Mark;
            Start = task.Start;
            End = task.End;
            Interval = $"{Start} - {End}";
        }

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
