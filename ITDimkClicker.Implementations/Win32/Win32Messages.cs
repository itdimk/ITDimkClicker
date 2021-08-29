using System;

namespace ITDimkClicker.Implementations.Win32
{
    [Flags]
    public enum Win32Messages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEHWHEEL = 0x020E,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_MOUSEACTIVATE = 0x0021
    }
}