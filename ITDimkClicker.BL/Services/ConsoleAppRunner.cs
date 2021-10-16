using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Services
{
    public class ConsoleAppRunner : IConsoleAppRunner
    {
        private string _appPath;
        private bool _isRunning;
        private Process _process;

        public event EventHandler IsRunningChanged;

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                _isRunning = value;
                IsRunningChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ConsoleAppRunner(string appPath)
        {
            _appPath = appPath;
        }

        public async void Run(string args)
        {
            var startInfo = new ProcessStartInfo()
            {
                Arguments = args,
                FileName = _appPath,
                UseShellExecute = true,
               WindowStyle = ProcessWindowStyle.Hidden,
               CreateNoWindow = true,
               
            };

            try
            {
                IsRunning = true;
                _process = Process.Start(startInfo);
                await _process.WaitForExitAsync();
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}