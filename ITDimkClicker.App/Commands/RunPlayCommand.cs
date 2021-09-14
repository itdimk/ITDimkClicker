using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public class RunPlayCommand : ICommand
    {
        private readonly IConsoleAppWrapper _wrapper;

        public RunPlayCommand(IConsoleAppWrapper wrapper)
        {
            _appPath = appPath;
            _processName = processName;
        }
        public bool CanExecute(object parameter) => !_wrapper.IsRunning;
        public void Execute(object parameter)
        {
            string fileName = (string) parameter;
            _wrapper.Run($"play -b S -bm Alt -i {fileName}");
        }

        public event EventHandler CanExecuteChanged;
    }
}