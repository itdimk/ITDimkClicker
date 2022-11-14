using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;
using ITDimkClicker.Common.Services;
using ITDImkClicker.ConsoleApp.Data;
using Ninject;
using Ninject.Modules;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

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
            try
            {
                base.OnStartup(e);

                _iocKernel = new KernelConfiguration(new IocConfiguration())
                    .BuildReadonlyKernel();

                Current.MainWindow = _iocKernel.Get<MainWindow>();
                Current.MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                File.WriteAllText("log.txt",$"{ex.Message}\n{ex.StackTrace}");
            }

        }
    }
}