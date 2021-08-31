using ITDimkClicker.Presentation.Presenters;
using LightInject;

namespace ITDimkClicker.Presentation
{
    public class AppController : IAppController
    {
        public IServiceContainer ServiceContainer { get; }

        public AppController(IServiceContainer serviceContainer)
        {
            ServiceContainer = serviceContainer;
        }

        public void Run<TPresenter>() where TPresenter : IPresenter
        {
            if (!ServiceContainer.CanGetInstance(typeof(TPresenter), string.Empty))
                ServiceContainer.Register<TPresenter>();

            TPresenter presenter = ServiceContainer.GetInstance<TPresenter>();
            presenter.Run();
        }
    }
}