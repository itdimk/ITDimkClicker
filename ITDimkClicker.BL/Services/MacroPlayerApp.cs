using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using ITDimkClicker.BL.Win32;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.BL.Services
{
    public class MacroPlayerApp : IMacroPlayerApp
    {
        private readonly InputSimulator _simulator = new();
        private readonly List<VirtualKeyCode> _pressedKeys = new();
        private readonly Stopwatch _stopwatch = new();

        private readonly Dictionary<RawMouseButtonFlags, MouseEvent.EventFlags> _mouseButtonsMap = new()
        {
            { RawMouseButtonFlags.LeftButtonDown, MouseEvent.EventFlags.LEFTDOWN },
            { RawMouseButtonFlags.LeftButtonUp, MouseEvent.EventFlags.LEFTUP },
            { RawMouseButtonFlags.RightButtonDown, MouseEvent.EventFlags.RIGHTDOWN },
            { RawMouseButtonFlags.RightButtonUp, MouseEvent.EventFlags.RIGHTUP },
            { RawMouseButtonFlags.MiddleButtonDown, MouseEvent.EventFlags.MIDDLEDOWN },
            { RawMouseButtonFlags.MiddleButtonUp, MouseEvent.EventFlags.MIDDLEUP }
        };

        private void PlayEvent(MacroEvent macroEvent)
        {
            if (macroEvent.Data is RawInputKeyboardData keyboardData)
                PlayKeyboardInputData(keyboardData.Keyboard);
            else if (macroEvent.Data is RawInputMouseData mouseData)
                PlayMouseInputData(mouseData.Mouse);
        }

        private void PlayKeyboardInputData(RawKeyboard keyboard)
        {
            var vk = (VirtualKeyCode)keyboard.VirutalKey;

            if ((keyboard.Flags & RawKeyboardFlags.Up) != 0)
            {
                _simulator.Keyboard.KeyUp(vk);
                _pressedKeys.Remove(vk);
            }
            else
            {
                _simulator.Keyboard.KeyDown(vk);
                if (!_pressedKeys.Contains(vk))
                    _pressedKeys.Add(vk);
            }
        }

        private void PlayMouseInputData(RawMouse mouseData)
        {
            if (mouseData.LastX != 0 || mouseData.LastY != 0)
                MouseEvent.SendEvent(MouseEvent.EventFlags.MOVE, mouseData.LastX, mouseData.LastY);

            if (mouseData.ButtonData != 0)
                MouseEvent.SendEvent(MouseEvent.EventFlags.WHEEL, 0, 0, mouseData.ButtonData);

            foreach (var flags in _mouseButtonsMap)
                if (mouseData.Buttons.HasFlag(flags.Key))
                    MouseEvent.SendEvent(flags.Value, 0, 0);
        }


        public void Run(Macro[] macro, CancellationToken token)
        {
            void Loop()
            {
                while (true)
                    foreach (var m in macro)
                    {
                        PlayMacro(m, token);
                        if (token.IsCancellationRequested) return;
                    }
            }

            Task.Factory.StartNew(Loop, token);
            Application.Run();
        }

        private void PlayMacro(Macro macro, CancellationToken token)
        {
            Cursor.Position = new Point(macro.InitMouseX, macro.InitMouseY);
            _stopwatch.Restart();

            foreach (var macroEvent in macro)
            {
                long waitTime = macroEvent.Timestamp - _stopwatch.ElapsedTicks;
                if (waitTime > 0)
                    Thread.Sleep(new TimeSpan(waitTime));

                PlayEvent(macroEvent);

                if (token.IsCancellationRequested)
                {
                    Dispose();
                    return;
                }
            }
        }

        public void Dispose()
        {
            MouseEvent.SendEvent(MouseEvent.EventFlags.LEFTUP, 0, 0);
            MouseEvent.SendEvent(MouseEvent.EventFlags.RIGHTUP, 0, 0);
            MouseEvent.SendEvent(MouseEvent.EventFlags.MIDDLEUP, 0, 0);
            _pressedKeys.ForEach(k => _simulator.Keyboard.KeyUp(k));
            Application.Exit();
        }
    }
}