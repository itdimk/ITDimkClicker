using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Services
{
    public class ConsoleAppWrapper : IConsoleAppWrapper
    {
        private readonly string _appFileName;
        private Process _appProcess;
        private bool _isRunnong;

        public event EventHandler IsRunningChanged;

        public bool IsRunning
        {
            get => _isRunnong;
            set
            {
                _isRunnong = value;
                IsRunningChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ConsoleAppWrapper(string appFileName)
        {
            _appFileName = appFileName;
        }
        
        public async void Run(string args)
        {
            var startInfo = new ProcessStartInfo(_appFileName)
            {
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true,
//                WindowStyle = ProcessWindowStyle.Hidden
            };

            _appProcess = Process.Start(startInfo);
            
            IsRunning = true;
            await _appProcess.WaitForExitAsync();
            IsRunning = false;
        }
    }
}