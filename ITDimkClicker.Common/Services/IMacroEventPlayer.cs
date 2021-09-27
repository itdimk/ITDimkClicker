using System;
using System.Diagnostics;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroEventPlayer 
    {
        void Play(MacroEvent target, long waitTime);
        void ReleaseKey(int vKeyCode);
    }
}