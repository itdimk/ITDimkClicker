using System;
using System.Threading;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroPlayer : IMacroPlayer
    {
        private void PlayEvent(MacroEvent macroEvent)
        {
            if (macroEvent.Data is RawInputKeyboardData keyboardData)
            {
            }
            else if (macroEvent.Data is RawInputMouseData mouseData)
            {
            }
        }

        public void RunLoop(Macro macro, CancellationToken token)
        {
            long timestamp = DateTime.Now.Ticks;

            foreach (var record in macro)
            {
                long timeLast = DateTime.Now.Ticks - timestamp;

                while (record.Timestamp < timeLast)
                    Thread.Sleep(1);

                PlayEvent(record);
            }
        }
    }
}