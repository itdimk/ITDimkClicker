using System;

namespace ITDimkClicker.App.Commands.Parameters
{
    public class CurrentFileAccessor
    {
        public Action<string> SetCurrentFile;
        public Func<string> GetCurrentFile;

        public CurrentFileAccessor(Action<string> setCurrentFile, Func<string> getCurrentFile)
        {
            SetCurrentFile = setCurrentFile;
            GetCurrentFile = getCurrentFile;
        }
    }
}