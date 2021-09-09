using System;
using System.Windows.Input;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class SaveFileCommand: ICommand
    {
        public event EventHandler<string> FileSelected;
        
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var dialog = new SaveFileDialog();
            bool? result = dialog.ShowDialog();
            
            if(result == true)
                FileSelected?.Invoke(this, dialog.FileName);
        }

        public event EventHandler? CanExecuteChanged;
    }
}