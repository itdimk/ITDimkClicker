using System.IO;
using ITDimkClicker.Common.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroIO
    {
        void Write(Stream output, params Macro[] macros);
        Macro[] ReadAll(Stream input);
        Macro ReadOne(Stream input);
    }
}