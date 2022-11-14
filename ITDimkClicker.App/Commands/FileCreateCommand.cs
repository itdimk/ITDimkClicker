using System;
using System.IO;
using ITDimkClicker.App.Commands.Parameters;
using ITDimkClicker.App.Services;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.Commands
{
    public class FileCreateCommand : BaseCommand
    {
        private readonly IMacroIO _io;

        public FileCreateCommand(IConsoleAppRunner runner, IMacroIO io) : base(runner)
        {
            _io = io;
        }

        public override void Execute(object parameter)
        {
            var accessor = (CurrentFileAccessor)parameter;
            
            string fileName = "New macro.bin";
            string fullPath = Path.Combine(Path.GetTempPath(), fileName);
            var macro = new Macro(0, 0);

            if(File.Exists(fullPath))
                File.Delete(fullPath);
            
            using var output = File.OpenWrite(fullPath);
            _io.Write(output, macro);
            accessor.SetCurrentFile(fullPath);
        }
    }
}