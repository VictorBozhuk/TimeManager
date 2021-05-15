using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    /// <summary>
    /// Interaction logic for CreateEditGlobalTaskViewModel.xaml
    /// </summary>
    public partial class CreateEditGlobalTask : Page
    {
        public CreateEditGlobalTask(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
