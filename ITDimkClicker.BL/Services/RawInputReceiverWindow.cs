using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.BL.Services
{
    public class RawInputReceiverWindow : NativeWindow, IRawInputReceiverWindow
    {
        public event EventHandler<RawInputData> Input;

        public RawInputReceiverWindow()
        {
            CreateHandle(new CreateParams
            {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
                Style = 0x800000,
            });
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_INPUT = 0x00FF;

            if (m.Msg == WM_INPUT)
                Input?.Invoke(this, RawInputData.FromHandle(m.LParam));

            base.WndProc(ref m);
        }

        public void Dispose()
        {
            base.DestroyHandle();
        }
    }
}