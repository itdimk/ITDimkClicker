using System.Linq;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.Common.Services;
using Microsoft.Win32;

namespace ITDimkClicker.App.Commands
{
    public class MergeCommand : BaseCommand
    {
        public MergeCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

        public override void Execute(object parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Macros files|*.bin",
                AddExtension = true,
                Multiselect = true
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var accessor = (CurrentFileAccessor)parameter;
                string inputFiles = dialog.FileNames.Select(f => " -i " + f)
                    .Aggregate((result, item) => result + item);
                
                Wrapper.Run($"merge -o \"{accessor.GetCurrentFile()}\" {inputFiles}");
            }
        }
    }
}