using System;
using System.IO;
using System.Linq;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Implementations.Extensions
{
    public static class MacroEx
    {
        public static void WriteToStream(this Macro macro, Stream output)
        {
            byte[] count = BitConverter.GetBytes(macro.Count);
            output.Write(count, 0, count.Length);
            
            foreach (var macroEvent in macro)
               macroEvent.WriteToStream(output);
        }

        public static Macro ReadFromStream(Stream source)
        {
            byte[] countBytes = new byte[sizeof(int)];
            source.Read(countBytes, 0, countBytes.Length);
            int count = BitConverter.ToInt32(countBytes);

            var result = new Macro();
            for (int i = 0; i < count; ++i)
               result.Add(MacroEventEx.ReadFromStream(source));
            
            return result;
        }
    }
}