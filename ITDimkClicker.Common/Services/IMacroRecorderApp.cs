using System;
using System.Threading;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroRecorderApp : IDisposable
    {
        Macro Run(CancellationToken token);
    }
}