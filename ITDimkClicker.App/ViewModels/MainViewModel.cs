using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.App.Commands;
using ITDimkClicker.BL.Services;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;

namespace ITDimkClicker.App.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private readonly IMacroFileManager _fileManager;
        private CancellationTokenSource _tokenSource;
        private readonly string _macroFileName;

        private Process _currentProcess;
        private KeyboardHook _hook;

        public OpenFileCommand OpenFile { get; }
        public SaveFileCommand SaveFile { get; }

        public MainViewModel(IMacroFileManager fileManager)
        {
            _fileManager = fileManager;
            _macroFileName = Path.Combine(Environment.CurrentDirectory, "macro.bin");

            OpenFile = new OpenFileCommand();
            OpenFile.FileSelected += OpenFileOnFileSelected;

            SaveFile = new SaveFileCommand();
            SaveFile.FileSelected += SaveFileOnFileSelected;

            _hook = new KeyboardHook();
            _hook.RegisterHotKey(ModifKeys.Alt, Keys.R);
            _hook.RegisterHotKey(ModifKeys.Alt, Keys.A);
            _hook.KeyPressed += OnKeyPressed;
        }

        private void SaveFileOnFileSelected(object? sender, string e)
        {
            if (File.Exists(_macroFileName))
                File.Copy(_macroFileName, e, true);
        }

        private void OpenFileOnFileSelected(object? sender, string e)
        {
            File.Copy(e, _macroFileName, true);
        }

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (_currentProcess?.HasExited == false) return;
            if (e.Key == Keys.A)
                RunPlayer();
            else if (e.Key == Keys.R)
                RunRecord();
        }

        private void RunRecord()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDimkClicker.ConsoleApp.lnk ")
            {
                Arguments = $"record -b S -bm Alt -o {_macroFileName}",
                UseShellExecute = true,
                CreateNoWindow = true,

                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
        }

        private void RunPlayer()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDimkClicker.ConsoleApp.lnk ")
            {
                Arguments = $"play -b S -bm Alt -i {_macroFileName}",
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
        }
    }
}