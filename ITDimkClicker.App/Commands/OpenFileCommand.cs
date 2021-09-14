using System;
using System.Windows.Input;
using ITDimkClicker.App.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class OpenFileCommand : InactiveOnRunCommand
    {
        public event EventHandler<string> FileSelected;

        public override void Execute(object parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Macros files|*.bin",
                AddExtension = true,
            };
            bool? result = dialog.ShowDialog();

            if (result == true)
                FileSelected?.Invoke(this, dialog.FileName);
        }

        public OpenFileCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }
    }
}