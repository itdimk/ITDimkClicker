using System.Collections.Generic;
using System.Text;

namespace ITDimkClicker.Abstractions.Data
{
    public class Macro : List<MacroEvent>
    {
        public readonly int StartX;
        public readonly int StartY;
        
        public Macro(int startX, int startY)
        {
            StartX = startX;
            StartY = startY;
        }
    }
}