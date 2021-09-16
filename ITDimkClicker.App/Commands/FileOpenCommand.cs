using System;
using System.Windows.Input;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class FileOpenCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Macros files|*.bin",
                AddExtension = true,
            };
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var accessor = (CurrentFileAccessor)parameter;
                accessor.SetCurrentFile(dialog.FileName);
            }
        }

        public FileOpenCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }
    }
}