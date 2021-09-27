using System;
using System.Diagnostics;
using System.Threading;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using WindowsInput;
using WindowsInput.Native;

namespace ITDimkClicker.BL.Services
{
    public class MacroEventPlayer : IMacroEventPlayer
    {
        private readonly InputSimulator _simulator;

        public MacroEventPlayer(InputSimulator simulator)
        {
            _simulator = simulator;
        }

        public void Play(MacroEvent target, long waitTime)
        {
            if (target.Data is RawInputKeyboardData keyboardData)
                PlayKeyboardInputData(keyboardData.Keyboard);
            else if (target.Data is RawInputMouseData mouseData)
                PlayMouseInputData(mouseData.Mouse);

            if (waitTime > 0)
                Thread.Sleep(new TimeSpan(waitTime));
        }

        public void ReleaseKey(int vKeyCode)
        {
            _simulator.Keyboard.KeyUp((VirtualKeyCode)vKeyCode);
        }


        private void PlayKeyboardInputData(RawKeyboard keyboard)
        {
            var vk = (VirtualKeyCode)keyboard.VirutalKey;

            if ((keyboard.Flags & RawKeyboardFlags.Up) != 0)
                _simulator.Keyboard.KeyUp(vk);
            else
                _simulator.Keyboard.KeyDown(vk);
        }

        private void PlayMouseInputData(RawMouse mouseData)
        {
            if (mouseData.LastX != 0 || mouseData.LastY != 0)
                _simulator.Mouse.MoveMouseBy(mouseData.LastX, mouseData.LastY);

            if (mouseData.ButtonData != 0)
                _simulator.Mouse.VerticalScroll(mouseData.ButtonData);

            var mButtons = mouseData.Buttons;

            if (mButtons == default) return;

            var _ = mButtons switch
            {
                RawMouseButtonFlags.LeftButtonDown => _simulator.Mouse.LeftButtonDown(),
                RawMouseButtonFlags.RightButtonDown => _simulator.Mouse.RightButtonDown(),
                RawMouseButtonFlags.MiddleButtonDown => _simulator.Mouse.MiddleButtonDown(),

                RawMouseButtonFlags.LeftButtonUp => _simulator.Mouse.LeftButtonUp(),
                RawMouseButtonFlags.RightButtonUp => _simulator.Mouse.RightButtonUp(),
                RawMouseButtonFlags.MiddleButtonUp => _simulator.Mouse.MiddleButtonUp(),
            };
        }
    }
}