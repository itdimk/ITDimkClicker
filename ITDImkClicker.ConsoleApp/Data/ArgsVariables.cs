using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using ITDimkClicker.Recorder.Utility;
using ITDimkClicker.RecorderApp.Utility;

namespace ITDimkClicker.Recorder.Data
{
    static class ArgsVariables
    {
        public enum Actions
        {
            Play,
            Record
        }

        public static Keys? BreakHotkey
        {
            get
            {
                string value = ArgsVariableGetter.Get(ArgsConstants.BREAK_HOTKEY);
                return Enum.TryParse(value, out Keys hotkey) ? hotkey : null;
            }
        }

        public static ModifKeys? BreakModifier
        {
            get
            {
                string value = ArgsVariableGetter.Get(ArgsConstants.BREAK_MODIFIER);
                return Enum.TryParse(value, out ModifKeys modif) ? modif : null;
            }
        }

        public static string OutputFileName => ArgsVariableGetter.Get(ArgsConstants.OUTPUT);
        public static string InputFileName => ArgsVariableGetter.Get(ArgsConstants.INPUT);

        public static Actions? Mode
        {
            get
            {
                string[] args = Environment.GetCommandLineArgs();
                
                if (Array.IndexOf(args, ArgsConstants.PLAY_MODE) != -1)
                    return Actions.Play;
                
                if (Array.IndexOf(args, ArgsConstants.RECORD_MODE) != -1)
                    return Actions.Record;

                return null;
            }
        }
    }
}