using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public class RunRecordCommand : ICommand
    {
        private readonly IConsoleAppWrapper _wrapper;

        public RunRecordCommand(IConsoleAppWrapper wrapper)
        {
            _wrapper = wrapper;
            _wrapper.IsRunningChanged += (_, _) 
                => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter) => !_wrapper.IsRunning;

        public void Execute(object parameter)
        {
            string fileName = (string) parameter;
            _wrapper.Run($"record -b S -bm Alt -o {fileName}");
        }

        public event EventHandler CanExecuteChanged;
    }
}