using System;
using System.Diagnostics;

namespace ITDimkClicker.App.Services
{
    public class ConsoleAppWrapper : IConsoleAppWrapper
    {
        private const string AppPath = "ITDimkClicker.ConsoleApp.lnk";
        private Process _appProcess;


        public event EventHandler IsRunningChanged;
        public bool IsRunning => _appProcess != null;

        public async void Run(string args)
        {
            var startInfo = new ProcessStartInfo(AppPath)
            {
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true,
                // WindowStyle = ProcessWindowStyle.Hidden
            };

            _appProcess = Process.Start(startInfo);
            await _appProcess.WaitForExitAsync();
            OnProcessExited(this, EventArgs.Empty);
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            IsRunningChanged?.Invoke(this, EventArgs.Empty);
            _appProcess.Dispose();
            _appProcess = null;
        }
    }
}