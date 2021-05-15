using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.ViewModels;

namespace TimeManager.Ioc
{
    public class ViewModelLocator
    {
        public static DailyTasksViewModel MainPageViewModel
        {
            get { return IocKernel.Get<DailyTasksViewModel>(); }
        }

        public static MainViewModel MainViewModel
        {
            get { return IocKernel.Get<MainViewModel>(); }
        }
    }
}
