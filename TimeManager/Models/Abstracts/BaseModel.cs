using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeManager.Abstracts;

namespace TimeManager.Models.Abstracts
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Brush StatusColor { get; set; }
        public Brush RowColor { get; set; }
        public double? HeightDescriptionBlock { get; set; } = null;

        public BaseModel() { }
        public BaseModel(int index = 1)
        {
            if (index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
        }

        protected void SetStatusColor()
        {

            if (Status == Statuses.Done)
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
        }
    }
}
