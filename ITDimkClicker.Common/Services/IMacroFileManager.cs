using System.IO;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroFileManager
    {
        void Write(Macro macros, Stream output);
        Macro Read(Stream input);
    }
}