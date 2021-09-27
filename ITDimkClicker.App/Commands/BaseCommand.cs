using System;
using System.Windows.Input;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected readonly IConsoleAppRunner Runner;
        public event EventHandler CanExecuteChanged;
        
        public BaseCommand(IConsoleAppRunner runner)
        {
            Runner = runner;
            Runner.IsRunningChanged += (_, e) => CanExecuteChanged?.Invoke(this, e);
        }

        public virtual bool CanExecute(object parameter) => !Runner.IsRunning;
        public abstract void Execute(object parameter);

    }
}