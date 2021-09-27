using System;
using System.Threading;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public abstract class MacroPlayer : IDisposable
    {
        protected readonly IMacroEventPlayer EventPlayer;
        
        public MacroPlayer(IMacroEventPlayer _eventPlayer)
        {
            EventPlayer = _eventPlayer;
        } 
        
        public abstract void Run(Macro[] macro, CancellationToken token);
        public abstract void Dispose();
        
    }
}