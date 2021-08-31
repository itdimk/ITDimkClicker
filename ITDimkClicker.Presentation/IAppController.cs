using ITDimkClicker.Presentation.Presenters;
using LightInject;

namespace ITDimkClicker.Presentation
{
    public interface IAppController
    {
        IServiceContainer ServiceContainer { get; }

        void Run<TPresenter>()
            where TPresenter : IPresenter;
    }
}