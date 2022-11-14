using ITDimkClicker.App.Services;
using ITDimkClicker.App.ViewModels;
using ITDimkClicker.BL.Services;
using ITDimkClicker.Common.Services;
using Ninject.Modules;

namespace ITDimkClicker.App
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IMacroIO>().To<MacroIO>().InSingletonScope(); // Reuse same storage every time
            Bind<IConsoleAppRunner>().ToConstant(new ConsoleAppRunner("ITDimkClicker.ConsoleApp.exe"));
            Bind<MainViewModel>().ToSelf().InTransientScope(); // Create new instance every time
        }
    }
}