using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.BL.Services
{
    public class MacroRecorderApp : IMacroRecorderApp
    {
        private readonly IRawInputReceiverWindow _receiver;
        private readonly Stopwatch _stopwatch = new();

        public MacroRecorderApp(IRawInputReceiverWindow receiver)
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

        public Macro Run(CancellationToken token)
        {
            var result = new Macro(Cursor.Position.X, Cursor.Position.Y);
            _receiver.Input += (_, e) => AddMacroEvent(result, e, token);

            try
            {
                _stopwatch.Restart();
                RegisterDevices();
                Application.Run();
            }
            finally
            {
                Dispose();
            }
            return result;
        }

        void AddMacroEvent(Macro result, RawInputData data, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
                result.Add(new MacroEvent(_stopwatch.ElapsedTicks, data));
            else
                Dispose();
        }

        public void Dispose()
        {
            UnregisterDevices();
            _receiver.Dispose();
            Application.Exit();
        }
    }
}