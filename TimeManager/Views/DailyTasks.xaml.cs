using System;
using System.Windows;
using System.Windows.Controls;
using TimeManager.Ioc;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    public partial class DailyTasks : Page
    {
        public DailyTasks(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void SelecteRow(object sender, RoutedEventArgs e)
        {
            GetParents(sender);
        }

        private void GetParents(Object element)
        {
            if (element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent != null)
                {
                    GetParents(((FrameworkElement)element).Parent);
                }
                else if (((FrameworkElement)element).TemplatedParent != null)
                {
                    GetParents(((FrameworkElement)element).TemplatedParent);
                }

                if (element is ListBoxItem)
                {
                    (element as ListBoxItem).IsSelected = true;
                }
            }
        }
    }
}
