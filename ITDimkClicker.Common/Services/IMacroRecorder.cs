using System;
using System.Threading;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroRecorder : IDisposable
    {
        Macro RunLoop(CancellationToken token);
    }
}