using System.Threading;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Abstractions.Services
{
    public interface IMacroPlayer
    {
        void RunLoop(Macro macro, CancellationToken token);
    }
}