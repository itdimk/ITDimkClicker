using System;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.BL.Services;

namespace ITDimkClicker.BL.Utility
{
    public class CancellationHotkey : IDisposable
    {
        private CancellationTokenSource _tokenSource;
        private ModifKeys _modifier;
        private KeyboardHook _hook;

        public CancellationHotkey(CancellationTokenSource tokenSource)
        {
            _tokenSource = tokenSource;
            _hook = new KeyboardHook();
        }

        public void Register(Keys breakHotkey, ModifKeys modifier)
        {
            _hook.RegisterHotKey(modifier | ModifKeys.Alt, breakHotkey);
            _hook.RegisterHotKey(modifier | ModifKeys.Control, breakHotkey);
            _hook.RegisterHotKey(modifier | ModifKeys.Shift, breakHotkey);
            _hook.RegisterHotKey((ModifKeys)0b111, breakHotkey);
            _hook.KeyPressed += HookOnKeyPressed;
            _modifier |= modifier;
        }

        private void HookOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modif.HasFlag(_modifier))
                _tokenSource.Cancel();
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
            _hook?.Dispose();
        }
    }
}
