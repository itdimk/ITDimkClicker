using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ITDimkClicker.App.Commands
{
    public class RunRecordCommand : ICommand
    {
        private readonly string _appPath;
        private readonly string _processName;

        public RunRecordCommand(string appPath, string processName)
        {
            _appPath = appPath;
            _processName = processName;
        }
        
        public bool CanExecute(object parameter)
        {
            return Process.GetProcessesByName(_processName).Length == 0;
        }

        public void Execute(object parameter)
        {
            string fileName = (string) parameter;
            var startInfo = new ProcessStartInfo(_appPath)
            {
                Arguments = $"record -b S -bm Alt -o {fileName}",
                UseShellExecute = true,
                CreateNoWindow = true,

                // WindowStyle = ProcessWindowStyle.Hidden
            };

           Process.Start(startInfo);
           CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}