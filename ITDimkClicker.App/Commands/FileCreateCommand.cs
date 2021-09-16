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
        private readonly IMacroFileManager _fileManager;

        public FileCreateCommand(IConsoleAppWrapper wrapper, IMacroFileManager fileManager) : base(wrapper)
        {
            _fileManager = fileManager;
        }

        public override void Execute(object parameter)
        {
            var accessor = (CurrentFileAccessor)parameter;
            
            string fileName = "New macro.bin";
            string fullPath = Path.Combine(Environment.CurrentDirectory, fileName);
            var macro = new Macro(0, 0);

            using var output = File.OpenWrite(fullPath);
            _fileManager.Write(macro, output);
            accessor.SetCurrentFile(fullPath);
        }
    }
}