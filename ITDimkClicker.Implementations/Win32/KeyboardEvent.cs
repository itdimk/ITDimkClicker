using System;
using System.Runtime.InteropServices;

namespace ITDimkClicker.Implementations.Win32
{
    public static class KeyboardEvent
    {
        [Flags]
        public enum KEYEVENTF
        {
            EXTENDEDKEY  = 0x0001,
            KEYEVENTF_KEYUP= 0x0002
        }

    [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
            IntPtr dwExtraInfo);
    }
}