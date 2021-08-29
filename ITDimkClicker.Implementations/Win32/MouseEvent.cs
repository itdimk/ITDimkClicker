using System;
using System.Runtime.InteropServices;

namespace ITDimkClicker.Implementations.Win32
{
    public static class MouseEvent
    {
        [Flags]
        public enum EventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x0800
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(
            int dwFlags, // motion and click options
            int dx, // horizontal position or change
            int dy, // vertical position or change
            int dwData, // wheel movement
            IntPtr dwExtraInfo // application-defined information
        );

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();
        
        public static void SendEvent(EventFlags value, int dx, int dy, int dwData = 0)
        {
            mouse_event( (int)value, dx, dy, dwData, GetMessageExtraInfo());
        }
    }
}