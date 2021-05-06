using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Storage.Storages;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels;

namespace TimeManager.Ioc
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IMyTaskStorage>().To<MyTaskStorage>().InSingletonScope();
            Bind<IDayStorage>().To<DayStorage>().InSingletonScope();

            Bind<MainPageViewModel>().ToSelf().InTransientScope();
            Bind<MainViewModel>().ToSelf().InTransientScope();
            Bind<CreateEditDayViewModel>().ToSelf().InTransientScope();
            Bind<CreateEditTaskViewModel>().ToSelf().InTransientScope();
        }
    }
}
