using System;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.RecorderApp.Utility;

namespace ITDimkClicker.Implementations.Utility
{
    public class CancellationHotkey : IDisposable
    {
        private CancellationTokenSource _tokenSource;
        private KeyboardHook _hook;

        public CancellationHotkey(CancellationTokenSource tokenSource)
        {
            _tokenSource = tokenSource;
            _hook = new KeyboardHook();
        }

        public void Register(Keys breakHotkey, ModifKeys modifier)
        {
            _hook.RegisterHotKey(modifier, breakHotkey);
            _hook.KeyPressed += HookOnKeyPressed;
        }

        private void HookOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            _tokenSource.Cancel();
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
            _hook?.Dispose();
        }
    }
}