using System;
using System.IO;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public class NewFileCommand : InactiveOnRunCommand
    {
        public event EventHandler<string> NewFileCreated;

        public NewFileCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

        public override void Execute(object parameter)
        {
            string fileName = "New macros.bin";
            string fullPath = Path.Combine(Environment.CurrentDirectory, fileName);
            File.Create(fullPath).Dispose();
            NewFileCreated?.Invoke(this, fullPath);
        }

    }
}