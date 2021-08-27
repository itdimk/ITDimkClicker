using System.Collections.Generic;
using System.Text;

namespace ITDimkClicker.Abstractions.Data
{
    public class Macro : List<MacroEvent>
    {
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var mouseEvent in this)
                stringBuilder.AppendLine(mouseEvent.ToString());

            return stringBuilder.ToString();
        }
    }
}