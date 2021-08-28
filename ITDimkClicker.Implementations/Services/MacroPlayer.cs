using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Win32;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroPlayer : IMacroPlayer
    {
        private void PlayEvent(MacroEvent macroEvent) // shit
        {
            if (macroEvent.Data is RawInputKeyboardData keyboardData)
            {
            }
            else if (macroEvent.Data is RawInputMouseData mouseData)
            {
                int lastX = mouseData.Mouse.LastX;
                int lastY = mouseData.Mouse.LastY;

                if (mouseData.Mouse.LastX != 0 || mouseData.Mouse.LastY != 0)
                    MouseEvent.SendEvent(MouseEvent.EventFlags.Move, lastX, lastY);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonDown))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.LeftDown, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonUp))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.LeftUp, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.RightButtonDown))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.RightDown, 0, 0);

                if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.RightButtonUp))
                    MouseEvent.SendEvent(MouseEvent.EventFlags.RightUp, 0, 0);

                if (mouseData.Mouse.ButtonData != 0)
                    MouseEvent.SendEvent(MouseEvent.EventFlags.MOUSEEVENTF_WHEEL, 0, 0, mouseData.Mouse.ButtonData);
            }
        }


        public void RunLoop(Macro macro, CancellationToken token)
        {
            
            Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Cursor.Position = new Point(macro.StartX, macro.StartY);
                    for (int i = 0; i < macro.Count; ++i)
                    {
                        PlayEvent(macro[i]);
                        if (i < macro.Count - 1)
                            Thread.Sleep(new TimeSpan(macro[i + 1].Timestamp - macro[i].Timestamp));

                        if (token.IsCancellationRequested)
                            break;
                    }
                }

                Application.Exit();
            }, token);

            Application.Run();
        }
    }
}