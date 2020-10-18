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
using TimeManager.Persistence.Repository;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    /// <summary>
    /// Interaction logic for Estimate.xaml
    /// </summary>
    public partial class Estimate : Page
    {
        public Estimate(IMyTaskStorage myTaskStorage, string id, ListOfMyTasksViewModel listOfMyTasksViewModel)
        {
            InitializeComponent();
            DataContext = new EstimateViewModel(myTaskStorage, id, listOfMyTasksViewModel);
        }
    }
}
