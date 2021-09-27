using System;
using System.Threading;
using ITDimkClicker.Common.Data;
using System.Windows.Forms;
using ITDImkClicker.ConsoleApp.Data;

namespace ITDimkClicker.Common.Services
{
    public interface IMacroRecorderApp : IDisposable
    {
        Macro Run(Keys breakKey, ModifKeys breakModifier);
    }
}