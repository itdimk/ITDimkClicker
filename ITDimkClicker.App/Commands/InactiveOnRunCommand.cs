using System;
using System.Windows.Input;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public abstract class InactiveOnRunCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        protected readonly IConsoleAppWrapper Wrapper;

        public InactiveOnRunCommand(IConsoleAppWrapper wrapper)
        {
            Wrapper = wrapper;
            Wrapper.IsRunningChanged += OnWrapperOnIsRunningChanged;
        }

        private void OnWrapperOnIsRunningChanged(object _, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public bool CanExecute(object parameter) => !Wrapper.IsRunning;

        public abstract void Execute(object parameter);

    }
}