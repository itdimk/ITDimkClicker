using System.IO;
using ITDimkClicker.Abstractions.Data;

namespace ITDimkClicker.Abstractions.Services
{
    public interface IMacroFileManager
    {
        void Write(Macro macros, Stream output);
        Macro Read(Stream output);
    }
}