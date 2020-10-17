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

        private RelayCommand listCommand;
        public RelayCommand ListCommand
        {
            get
            {
                return listCommand ??
                  (listCommand = new RelayCommand(obj =>
                  {
                      MenuPage = new ListOfMyTasks();
                  }));
            }
        }

        private RelayCommand createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return createCommand ??
                  (createCommand = new RelayCommand(obj =>
                  {
                      MenuPage = new CreateMyTasks();
                  }));
            }
        }

        private RelayCommand analyticsCommand;
        public RelayCommand AnalyticsCommand
        {
            get
            {
                return analyticsCommand ??
                  (analyticsCommand = new RelayCommand(obj =>
                  {
                      MenuPage = new Analytics();
                  }));
            }
        }
    }
}
