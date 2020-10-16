using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {


        private RelayCommand listCommand;
        public RelayCommand ListCommand
        {
            get
            {
                return listCommand ??
                  (listCommand = new RelayCommand(obj =>
                  {
                      
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

                  }));
            }
        }
    }
}
