using System.IO;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Abstractions.Services
{
    public interface IMacroFileManager
    {
        void Save(Macro macros, Stream output);
        Macro Load(Stream output);
    }
}