using System;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Common.Services
{
    public interface IRawInputReceiverWindow : IDisposable
    {
        event EventHandler<RawInputData> Input;
        IntPtr Handle { get; }
    }
}