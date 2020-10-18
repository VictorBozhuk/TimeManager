using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Views;

namespace TimeManager.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private Page menuPage;

        public MenuViewModel()
        {
            MenuPage = new ListOfMyTasks();
        }

        public Page MenuPage
        {
            get { return menuPage; }
            set
            {
                menuPage = value;
                OnPropertyChanged(nameof(MenuPage));
            }
        }

        public RelayCommand ListCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      MenuPage = new ListOfMyTasks();
                  });
            }
        }

        public RelayCommand CreateCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      MenuPage = new CreateMyTasks();
                  });
            }
        }

        public RelayCommand AnalyticsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      MenuPage = new Analytics();
                  });
            }
        }
    }
}
