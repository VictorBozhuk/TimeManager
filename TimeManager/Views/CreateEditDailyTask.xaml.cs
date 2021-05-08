using System.Windows.Controls;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    public partial class CreateEditDailyTask : Page
    {
        public CreateEditDailyTask(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
