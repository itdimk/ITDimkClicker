using System.IO;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Extensions;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroFileManager : IMacroFileManager
    {
        public void Save(Macro macros, Stream output)
        {
            macros.WriteToStream(output);
        }

        public Macro Load(Stream output)
        {
            return MacroEx.ReadFromStream(output);
        }
    }
}