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
        Bind<IMacroFileManager>().To<MacroFileManager>().InSingletonScope(); // Reuse same storage every time
        Bind<IConsoleAppWrapper>().To<ConsoleAppWrapper>().InSingletonScope();

        Bind<MainViewModel>().ToSelf().InTransientScope(); // Create new instance every time
    }
}
}