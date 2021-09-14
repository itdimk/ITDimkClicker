using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public class RunRecordCommand : InactiveOnRunCommand
    {
        public RunRecordCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

        public override void Execute(object parameter)
        {
            string fileName = (string) parameter;
            Wrapper.Run($"record -b S -bm Alt -o \"{fileName}\"");
        }
    }
}