using System;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IRawInputReceiverWindow : IDisposable
    {
        event EventHandler<RawInputEventArgs> Input;
        IntPtr Handle { get; }
    }
}