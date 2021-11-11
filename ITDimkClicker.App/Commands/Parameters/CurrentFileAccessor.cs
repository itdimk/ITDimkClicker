using System;

namespace ITDimkClicker.App.Commands.Parameters
{
    public class CurrentFileAccessor
    {
        public Action<float> SetPlayingSpeed;
        public Action<string> SetCurrentFile;
        public Func<string> GetCurrentFile;
        public Func<float> GetPlayingSpeed;

        public CurrentFileAccessor(Action<string> setCurrentFile, Func<string> getCurrentFile, Action<float> setPlayingSpeed,
            Func<float> getPlayingSpeed)
        {
            GetPlayingSpeed = getPlayingSpeed;
            SetPlayingSpeed = setPlayingSpeed;
            SetCurrentFile = setCurrentFile;
            GetCurrentFile = getCurrentFile;
        }
    }
}