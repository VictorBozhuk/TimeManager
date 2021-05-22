using SourceWeave.Controls;
using System.Windows;
using TimeManager.Ioc;
using TimeManager.ViewModels;

namespace TimeManager
{
    public partial class MainWindow : SWWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.MainViewModel;
        }
    }
}
