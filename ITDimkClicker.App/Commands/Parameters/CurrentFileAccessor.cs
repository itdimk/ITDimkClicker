using System;

namespace ITDimkClicker.App.Commands.Parameters
{
    public class CurrentFileAccessor
    {
        public float PlayingSpeed { get; set; }
        public Action<string> SetCurrentFile;
        public Func<string> GetCurrentFile;

        public CurrentFileAccessor(Action<string> setCurrentFile, Func<string> getCurrentFile, float playingSpeed)
        {
            PlayingSpeed = playingSpeed;
            SetCurrentFile = setCurrentFile;
            GetCurrentFile = getCurrentFile;
        }
    }
}