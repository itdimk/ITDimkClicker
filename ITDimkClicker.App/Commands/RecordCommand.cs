using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Commands
{
    public class RecordCommand : BaseCommand
    {
        public RecordCommand(IConsoleAppRunner runner) : base(runner)
        {
        }

        public override void Execute(object parameter)
        {
            var accessor = (CurrentFileAccessor)parameter;
            Runner.Run($"record -b S -bm Alt -o \"{accessor.GetCurrentFile()}\"");
        }
    }
}