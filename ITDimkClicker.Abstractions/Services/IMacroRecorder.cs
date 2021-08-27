using System;
using System.Threading;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Abstractions.Services
{
    public interface IMacroRecorder 
    {
        Macro RunLoop(CancellationToken token);
    }
}