using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Win32;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroPlayer : IMacroPlayer
    {
        InputSimulator simulator = new InputSimulator();
        private List<VirtualKeyCode> PressedKeys = new List<VirtualKeyCode>();

        private void PlayEvent(MacroEvent macroEvent) // shit
        {
            if (macroEvent.Data is RawInputKeyboardData keyboardData)
            {
                var vk = (VirtualKeyCode) keyboardData.Keyboard.VirutalKey;

                if ((keyboardData.Keyboard.Flags & RawKeyboardFlags.Up) != 0)
                {
                    simulator.Keyboard.KeyUp(vk);
                    PressedKeys.RemoveAll(k => k == vk);
                }
                else
                {
                    simulator.Keyboard.KeyDown(vk);
                    if(!PressedKeys.Contains(vk))
                        PressedKeys.Add(vk);
                }
            }
            else if (macroEvent.Data is RawInputMouseData mouseData)
            {
                int lastX = mouseData.Mouse.LastX;
                int lastY = mouseData.Mouse.LastY;

                if (mouseData.Mouse.LastX != 0 || mouseData.Mouse.LastY != 0)
                    MouseEvent.SendEvent(MouseEvent.EventFlags.MOVE, lastX, lastY);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonDown))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.LEFTDOWN, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonUp))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.LEFTUP, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.RightButtonDown))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.RIGHTDOWN, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.RightButtonUp))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.RIGHTUP, 0, 0);

                if (mouseData.Mouse.ButtonData != 0)
                    MouseEvent.SendEvent(MouseEvent.EventFlags.WHEEL, 0, 0, mouseData.Mouse.ButtonData);
            }
        }


        public void RunLoop(Macro macro, CancellationToken token)
        {
            Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Cursor.Position = new Point(macro.StartX, macro.StartY);
                    PlayMacro(macro, token);
                }

                Application.Exit();
            }, token);

            Application.Run();
        }

        private void PlayMacro(Macro macro, CancellationToken token)
        {
            var s = new Stopwatch();
            s.Start();

            foreach (var macroEvent in macro)
            {
                long waitTime = macroEvent.Timestamp - s.ElapsedTicks;
                if (waitTime > 0)
                    Thread.Sleep(new TimeSpan(waitTime));
                PlayEvent(macroEvent);

                if (token.IsCancellationRequested)
                {
                   PressedKeys.ForEach(k => simulator.Keyboard.KeyUp(k)); 
                    return;
                }
            }
        }
    }
}