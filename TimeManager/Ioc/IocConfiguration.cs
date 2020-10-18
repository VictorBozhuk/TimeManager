using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Persistence.Repository;
using TimeManager.ViewModels;

namespace TimeManager.Ioc
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IMyTaskStorage>().To<MyTaskStorage>().InSingletonScope();
            
            Bind<ListOfMyTasksViewModel>().ToSelf().InTransientScope();
            Bind<MenuViewModel>().ToSelf().InTransientScope();
        }
    }
}
