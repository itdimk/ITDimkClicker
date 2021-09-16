using System;

namespace ITDimkClicker.Common.Services
{
    public interface IConsoleAppWrapper
    {
        event EventHandler IsRunningChanged;
        bool IsRunning { get; }
        void Run(string args);
    }
}