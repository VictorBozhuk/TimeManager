using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeManager.Abstracts
{
    public class Statuses
    {
        public static readonly string All = "All";
        public static readonly string Done = "Done";
        public static readonly string NotDone = "Not done";
        public static readonly string InProgress = "In progress";

        public static readonly Brush ColorDone = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff196d13"));
        public static readonly Brush ColorNotDone = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffd61515"));
        public static readonly Brush ColorInProgress = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffbec01f"));
    }
}
