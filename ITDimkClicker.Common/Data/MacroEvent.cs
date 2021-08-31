using System;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Common.Data
{
    public class MacroEvent
    {
        public long Timestamp { get; }
        public RawInputData Data { get; }

        public MacroEvent(long timestamp, RawInputData data)
        {
            Timestamp = timestamp;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}