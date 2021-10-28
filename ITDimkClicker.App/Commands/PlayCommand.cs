using System;
using System.Diagnostics;
using System.Windows.Input;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Commands
{
    public class PlayCommand : BaseCommand
    {
        public PlayCommand(IConsoleAppRunner runner) : base(runner)
        {
        }

        public override void Execute(object parameter)
        {
            var accessor = (CurrentFileAccessor)parameter;
            Runner.Run($"play -b S -bm Alt -i \"{accessor.GetCurrentFile()}\" -s {accessor.PlayingSpeed}");
        }
    }
}