using System.Windows.Input;
using ITDimkClicker.App.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class ConcatCommand : BaseCommand
    {
        public ConcatCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

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
                string outputFilename = (string) parameter;
                Wrapper.Run($"concat -i \"{dialog.FileName}\" -o \"{outputFilename}\"");
            }
        }
    }
}