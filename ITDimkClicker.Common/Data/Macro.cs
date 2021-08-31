using System.Collections.Generic;
using System.Text;

namespace ITDimkClicker.Common.Data
{
    public class Macro : List<MacroEvent>
    {
        public readonly int InitMouseX;
        public readonly int InitMouseY;
        
        public Macro(int initMouseX, int initMouseY)
        {
            InitMouseX = initMouseX;
            InitMouseY = initMouseY;
        }
    }
}