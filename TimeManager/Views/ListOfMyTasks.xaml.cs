﻿using CommonServiceLocator;
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
using TimeManager.Ioc;
using TimeManager.ViewModels;

namespace TimeManager.Views
{
    /// <summary>
    /// Interaction logic for ListOfMyTasks.xaml
    /// </summary>
    public partial class ListOfMyTasks : Page
    {
        public ListOfMyTasks()
        {
            InitializeComponent();
            DataContext = IocKernel.Get<ListOfMyTasksViewModel>();
        }

        private void DB(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("d");
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
