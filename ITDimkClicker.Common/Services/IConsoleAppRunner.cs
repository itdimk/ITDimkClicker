using System;

namespace ITDimkClicker.Common.Services
{
    public  interface IConsoleAppRunner
    {
        event EventHandler IsRunningChanged;
        bool IsRunning { get; }
        
        void Run(string args);
    }
}