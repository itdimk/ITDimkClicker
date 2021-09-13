using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ITDimkClicker.App.Commands
{
    public class RunPlayCommand : ICommand
    {
        private readonly string _appPath;
        private readonly string _processName;

        public RunPlayCommand(string appPath, string processName)
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
                Arguments = $"play -b S -bm Alt -i {fileName}",
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