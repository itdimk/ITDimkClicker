using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Services;

namespace ITDimkClicker.App.Commands
{
    public class RunPlayCommand : InactiveOnRunCommand
    {
        public RunPlayCommand(IConsoleAppWrapper wrapper) : base(wrapper)
        {
        }

        public override void Execute(object parameter)
        {
            string fileName = (string) parameter;
            Wrapper.Run($"play -b S -bm Alt -i \"{fileName}\"");
        }
    }
}