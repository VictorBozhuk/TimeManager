using System.Windows;
using TimeManager.Ioc;
using TimeManager.ViewModels;

namespace TimeManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.MainViewModel;
        }
    }
}
