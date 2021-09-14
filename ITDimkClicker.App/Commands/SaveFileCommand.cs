using System;
using System.Windows.Input;
using ITDimkClicker.App.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class SaveFileCommand : InactiveOnRunCommand
    {
        public event EventHandler<string> FileSelected;


        public SaveFileCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

        public override void Execute(object parameter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Macros files|*.bin",
                AddExtension = true
            };
            bool? result = dialog.ShowDialog();

            if (result == true)
                FileSelected?.Invoke(this, dialog.FileName);
        }
    }
}