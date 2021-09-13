using System;
using System.IO;
using System.Windows.Input;

namespace ITDimkClicker.App.Commands
{
    public class NewFileCommand : ICommand
    {
        public event EventHandler<string> NewFileCreated;
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string fileName = Path.GetRandomFileName() + ".bin";
            string fullPath = Path.Combine(Environment.CurrentDirectory, fileName);
            File.Create(fullPath);
            NewFileCreated?.Invoke(this, fullPath);
        }

        public event EventHandler CanExecuteChanged;
    }
}