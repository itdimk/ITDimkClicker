using System;
using System.Threading;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroPlayerApp : IDisposable
    {
        void Run(Macro[] macro, CancellationToken token);
    }
}