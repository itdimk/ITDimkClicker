using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Extensions;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroRecorder : IMacroRecorder
    {
        private readonly IRawInputReceiverWindow _receiver;

        public MacroRecorder(IRawInputReceiverWindow receiver)
        {
            _receiver = receiver;
        }

        private void RegisterDevices()
        {
            RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard,
                RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, _receiver.Handle);
            RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse,
                RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, _receiver.Handle);
        }

        private void UnregisterDevices()
        {
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
        }

        public Macro RunLoop(CancellationToken token)
        {
            var result = new Macro();
            long startTimeStamp = DateTime.Now.Ticks;
            
            _receiver.Input += ReceiverOnInput;
            
            void ReceiverOnInput(object sender, RawInputEventArgs e)
            {
                result.Add(new MacroEvent(DateTime.Now.Ticks - startTimeStamp, e.Data));

                if (!token.IsCancellationRequested) return;
                _receiver.Input -= ReceiverOnInput;
                Application.Exit();
            }

            try
            {
                RegisterDevices();
                Application.Run();
            }
            finally
            {
                UnregisterDevices();
            }

            return result;
        }
    }
}