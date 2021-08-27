using System;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Abstractions.Services
{
    public interface IRawInputReceiverWindow 
    {
        event EventHandler<RawInputEventArgs> Input;
        IntPtr Handle { get; }
    }
}