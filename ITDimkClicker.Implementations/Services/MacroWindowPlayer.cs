using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Win32;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroWindowPlayer : IMacroPlayer
    {
        private IntPtr _hwnd;

        public MacroWindowPlayer(IntPtr hwnd)
        {
            _hwnd = hwnd;
        }

        public void RunLoop(Macro macro, CancellationToken token)
        {
            var s = new Stopwatch();
            s.Start();

            accX = macro.StartX;
            accY = macro.StartY;

            foreach (var macroEvent in macro)
            {
                if (macroEvent.Data is RawInputMouseData mouseData)
                {
                    accX += mouseData.Mouse.LastX;
                    accY += mouseData.Mouse.LastY;
                }

                long waitTime = macroEvent.Timestamp - s.ElapsedTicks;
                if (waitTime > 0)
                    Thread.Sleep(new TimeSpan(waitTime));
                PlayEvent(macroEvent);

                if (token.IsCancellationRequested)
                    return;
            }
        }

        private int accX, accY;

        private void PlayEvent(MacroEvent macroEvent) // shit
        {
            if (_hwnd == default) return;

            var children = GetChildWindows(_hwnd);

            //foreach (var c in children)
            {
                IntPtr hWndC = _hwnd; //c;

                var rect = WindowsInterop.GetWindowRect(hWndC, out WindowsInterop.RECT winRect);
                if (macroEvent.Data is RawInputMouseData mouseData)
                {

                    IntPtr lparam = WindowsInterop.MAKELPARAM(accX, accY);

                    if (mouseData.Mouse.LastX != 0 || mouseData.Mouse.LastY != 0)
                    {
                        bool result = WindowsInterop.SendMessage(hWndC, (int) Win32Messages.WM_MOUSEMOVE, 0x0000,
                            lparam.ToInt64());
                    }

                    if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonDown))
                        WindowsInterop.PostMessage(hWndC, (int) Win32Messages.WM_LBUTTONDOWN, 0x0001,
                            lparam.ToInt32());

                    if (mouseData.Mouse.Buttons.HasFlag(RawMouseButtonFlags.LeftButtonUp))
                        WindowsInterop.SendMessage(hWndC, (int) Win32Messages.WM_LBUTTONUP, 0x0001,
                            lparam.ToInt32());
                }
            }
        }


        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                WindowsInterop.Win32Callback childProc = new WindowsInterop.Win32Callback(EnumWindow);
                WindowsInterop.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            return result;
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");

            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }
    }
}