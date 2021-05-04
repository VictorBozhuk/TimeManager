using System.Windows.Controls;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    public partial class CreateEditTask : Page
    {
        public CreateEditTask(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
