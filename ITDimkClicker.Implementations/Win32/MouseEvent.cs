using System;
using System.Runtime.InteropServices;

namespace ITDimkClicker.Implementations.Win32
{
    public static class MouseEvent
    {
        [Flags]
        public enum EventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010,
            MOUSEEVENTF_WHEEL = 0x0800
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