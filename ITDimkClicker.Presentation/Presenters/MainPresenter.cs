using System;
using System.Diagnostics;
using System.Windows.Forms;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;
using ITDimkClicker.Common.Services;
using ITDimkClicker.Presentation.View;

namespace ITDimkClicker.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private readonly IMainView _view;
        private readonly IMacroFileManager _fileManager;
        private readonly IMacroPlayer _player;
        private readonly IMacroRecorder _recorder;
        private readonly KeyboardHook _hook;

        private Process _consoleApp;

        public MainPresenter(IAppController appController, IMainView view, IMacroFileManager fileManager,
            IMacroPlayer player, IMacroRecorder recorder, KeyboardHook hook) : base(appController, view)
        {
            _view = view;
            _fileManager = fileManager;
            _player = player;
            _recorder = recorder;
            _hook = hook;

            hook.RegisterHotKey(ModifKeys.Alt, Keys.R);
            hook.RegisterHotKey(ModifKeys.Alt, Keys.A);
            hook.KeyPressed += HookOnKeyPressed;
        }

        private void HookOnKeyPressed(object? sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.R && _consoleApp?.HasExited != false)
                RunRecord();

            if (e.Key == Keys.A && _consoleApp?.HasExited != false)
                RunPlayer();
        }


        private void RunRecord()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDImkClicker.ConsoleApp.exe ")
            {
                Arguments = "record -b S -bm Alt -o macros.bin",
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            _consoleApp = Process.Start(startInfo);
        }

        private void RunPlayer()
        {
            var startInfo = new ProcessStartInfo("ConsoleApp\\ITDImkClicker.ConsoleApp.exe ")
            {
                Arguments = "play -b S -bm Alt -i macros.bin",
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            _consoleApp = Process.Start(startInfo);
        }

        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}