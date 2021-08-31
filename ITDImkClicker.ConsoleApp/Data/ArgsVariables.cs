using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;

namespace ITDimkClicker.Recorder.Data
{
    static class ArgsVariables
    {
        public enum Actions
        {
            Play,
            Record
        }

        public static string OutputFileName => ArgsVariableGetter.Get(ArgsConstants.OUTPUT, true);
        public static string InputFileName => ArgsVariableGetter.Get(ArgsConstants.INPUT, true);
        
        public static Keys BreakHotkey
        {
            get
            {
                string value = ArgsVariableGetter.Get(ArgsConstants.BREAK_HOTKEY, true);
                return Enum.TryParse(value, out Keys hotkey)
                    ? hotkey
                    : throw new Exception($"Invalid break hotkey: {value}");
            }
        }

        public static ModifKeys BreakModifier
        {
            get
            {
                string value = ArgsVariableGetter.Get(ArgsConstants.BREAK_MODIFIER, true);
                return Enum.TryParse(value, out ModifKeys modif)
                    ? modif
                    : throw new Exception($"Invalid break modifier: {value}");
            }
        }

        public static Actions Mode
        {
            get
            {
                string[] args = Environment.GetCommandLineArgs();

                if (Array.IndexOf(args, ArgsConstants.PLAY_MODE) != -1)
                    return Actions.Play;

                if (Array.IndexOf(args, ArgsConstants.RECORD_MODE) != -1)
                    return Actions.Record;

                throw new Exception("\"play\" or \"record\" mode is expected");
            }
        }
    }
}