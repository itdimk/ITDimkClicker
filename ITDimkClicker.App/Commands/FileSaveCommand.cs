﻿using System;
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
        public FileSaveCommand(IConsoleAppRunner runner) : base(runner)
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
                string fileName = dialog.FileName;
                var accessor = (CurrentFileAccessor) parameter;
                Runner.Run($"merge -i \"{accessor.GetCurrentFile()}\" -o \"{fileName}\" -s {accessor.GetPlayingSpeed()}");
                accessor.SetCurrentFile(fileName);
                accessor.SetPlayingSpeed(1f);
            }
        }
    }
}