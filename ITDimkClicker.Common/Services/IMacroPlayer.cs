using System;
using System.Threading;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroPlayer : IDisposable
    {
        void RunLoop(Macro macro, CancellationToken token);
    }
}