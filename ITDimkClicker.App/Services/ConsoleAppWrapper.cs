using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITDimkClicker.App.Services
{
    public class ConsoleAppWrapper : IConsoleAppWrapper
    {
        private const string AppPath = "ITDimkClicker.ConsoleApp.lnk";
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

        public async void Run(string args)
        {
            var startInfo = new ProcessStartInfo(AppPath)
            {
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            _appProcess = Process.Start(startInfo);
            
            IsRunning = true;
            await _appProcess.WaitForExitAsync();
            IsRunning = false;
        }
    }
}