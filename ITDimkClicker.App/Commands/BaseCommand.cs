using System;
using System.Windows.Input;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected readonly IConsoleAppWrapper Wrapper;
        public event EventHandler CanExecuteChanged;
        
        public BaseCommand(IConsoleAppWrapper wrapper)
        {
            Wrapper = wrapper;
            Wrapper.IsRunningChanged += (_, e) => CanExecuteChanged?.Invoke(this, e);
        }

        public virtual bool CanExecute(object parameter) => !Wrapper.IsRunning;
        public abstract void Execute(object parameter);

    }
}