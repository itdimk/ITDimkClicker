using System;
using System.Diagnostics;
using System.Windows.Forms;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using ITDImkClicker.ConsoleApp.Data;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.BL.Services
{
    public class MacroRecorderApp : IMacroRecorderApp
    {
        private readonly IRawInputReceiverWindow _receiver;
        private readonly Stopwatch _stopwatch = new();
        private ModifKeys ModifiersPressed;
        private Macro temp;


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

        public Macro Run(Keys breakKey, ModifKeys breakModifier)
        {
            var result = new Macro(Cursor.Position.X, Cursor.Position.Y);
            _receiver.Input += (_, e) => AddMacroEvent(result, e, breakKey, breakModifier);

            try
            {
                RegisterDevices();
                _stopwatch.Restart();
                Application.Run();
            }
            finally
            {
                Dispose();
            }

            return result;
        }

        void AddMacroEvent(Macro result, RawInputData data, Keys breakKey, ModifKeys breakModifier)
        {
            SetPressedModifiers(data);
            if (!IsCancellationRequired(data, breakKey, breakModifier))
                result.Add(new MacroEvent(_stopwatch.ElapsedTicks, data));
            else
                Dispose();
        }

        void SetPressedModifiers(RawInputData data)
        {
            if (data is not RawInputKeyboardData keyboardData) return;
            bool isKeyDown = !keyboardData.Keyboard.Flags.HasFlag(RawKeyboardFlags.Up);

            void SetModifier(ModifKeys key)
            {
                if (isKeyDown)
                    ModifiersPressed |= key;
                else
                    ModifiersPressed &= ~key;
            }

            switch (keyboardData.Keyboard.VirutalKey)
            {
                case 0x12: // VK_MENU
                    SetModifier(ModifKeys.Alt);
                    break;
                case 0x11: // VK_CONTROL
                    SetModifier(ModifKeys.Control);
                    break;
                case 0x10: // VK_SHIFT
                    SetModifier(ModifKeys.Shift);
                    break;
            }
        }

        private bool IsCancellationRequired(RawInputData data, Keys breakKey, ModifKeys breakModifier)
        {
            if (data is RawInputKeyboardData keyboardData)
                return ModifiersPressed.HasFlag(breakModifier) && keyboardData.Keyboard.VirutalKey == (int)breakKey;
            return false;
        }
        
        public void Dispose()
        {
            UnregisterDevices();
            _receiver.Dispose();
            Application.Exit();
        }
    }
}