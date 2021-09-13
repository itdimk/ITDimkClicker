using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using Ninject.Modules;

namespace ITDimkClicker.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IReadOnlyKernel _iocKernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _iocKernel = new KernelConfiguration(new IocConfiguration())
                .BuildReadonlyKernel();

            Current.MainWindow = _iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}