using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeManager.Ioc;

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
            IocKernel.Initialize(new IocConfiguration());
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    IocKernel.Initialize(new IocConfiguration());

        //    base.OnStartup(e);
        //}

        //protected void InitializeDependencies(Module platformModule)
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterModule(platformModule);
        //    var locator = new AutofacServiceLocator(builder.Build());
        //    ServiceLocator.SetLocatorProvider(() => locator);
        //}
    }
}
