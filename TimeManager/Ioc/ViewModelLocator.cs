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
        public ListOfMyTasksViewModel ListOfMyTasksViewModel
        {
            get { return IocKernel.Get<ListOfMyTasksViewModel>(); }
        }
    }
}
