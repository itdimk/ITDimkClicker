using System;
using System.Windows.Forms;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Implementations.Services
{
    public class RawInputReceiverWindow : NativeWindow, IRawInputReceiverWindow
    {
        public event EventHandler<RawInputEventArgs> Input;

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
            {
                var data = RawInputData.FromHandle(m.LParam);

                Input?.Invoke(this, new RawInputEventArgs(data));
            }

            base.WndProc(ref m);
        }
    }
}