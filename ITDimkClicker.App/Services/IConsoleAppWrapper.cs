using System;

namespace ITDimkClicker.App.Services
{
    public interface IConsoleAppWrapper
    {
        public event EventHandler IsRunningChanged;
        public bool IsRunning { get; }
        public void Run(string args);
    }
}