using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.BL.Services
{
    public class MacroPlayerApp : MacroPlayer
    {
        private readonly Stopwatch _stopwatch = new();
        private HashSet<VirtualKeyCode> _pressedKeys = new();

        public MacroPlayerApp(IMacroEventPlayer player) : base(player)
        {
        }

        public override void Run(Macro[] macros, CancellationToken token)
        {
            Task.Factory.StartNew(() => RunAll(macros, token), token);
            Application.Run();
        }

        private void RunAll(Macro[] macros, CancellationToken token)
        {
            while (true)
                foreach (var macro in macros)
                {
                    if (token.IsCancellationRequested)
                    {
                        Dispose();
                        return;
                    }

                    RunOne(macro, token);
                }
        }

        private void RunOne(Macro macro, CancellationToken token)
        {
            Cursor.Position = new Point(macro.InitMouseX, macro.InitMouseY);
            _stopwatch.Restart();

            foreach (var macroEvent in macro)
                if (!token.IsCancellationRequested)
                {
                    EventPlayer.Play(macroEvent, macroEvent.Timestamp - _stopwatch.ElapsedTicks);
                    UpdatePressedKeys(macroEvent);
                }

            ReleasePressedKeys();
        }

        private void UpdatePressedKeys(MacroEvent e)
        {
            if (e.Data is not RawInputKeyboardData keyboardData) return;

            var vKeyCode = (VirtualKeyCode)keyboardData.Keyboard.VirutalKey;

            if (keyboardData.Keyboard.Flags == RawKeyboardFlags.Up)
                _pressedKeys.Remove(vKeyCode);
            else
                _pressedKeys.Add(vKeyCode);
        }

        private void ReleasePressedKeys()
        {
            foreach (var key in _pressedKeys)
                EventPlayer.ReleaseKey((int)key);
            _pressedKeys.Clear();
        }

        public override void Dispose()
        {
            ReleasePressedKeys();
            Application.Exit();
        }
    }
}