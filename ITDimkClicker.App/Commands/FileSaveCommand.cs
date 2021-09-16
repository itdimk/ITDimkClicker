using System;
using System.IO;
using System.Windows.Input;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class FileSaveCommand : BaseCommand
    {
        public FileSaveCommand(IConsoleAppWrapper wrapper) : base(wrapper)
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
            {
                var accessor = (CurrentFileAccessor)parameter;
                File.Copy(accessor.GetCurrentFile(), dialog.FileName);
            }
        }
    }
}