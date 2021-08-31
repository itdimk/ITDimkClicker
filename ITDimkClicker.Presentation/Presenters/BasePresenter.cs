using ITDimkClicker.Presentation.View;

namespace ITDimkClicker.Presentation.Presenters
{
    public class BasePresenter<TView> : IPresenter
        where TView : IView
    {
        protected TView View;
        protected IAppController AppController;

        public BasePresenter(IAppController appController, TView view)
        {
            AppController = appController;
            View = view;
        }

        public virtual void Run()
        {
            View.Show();
        }
    }
}