using System;
using System.Collections.Generic;
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
            Record,
            Concat
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

                if (args.Contains(ArgsConstants.PLAY_MODE))
                    return Actions.Play;

                if (args.Contains(ArgsConstants.RECORD_MODE))
                    return Actions.Record;

                if (args.Contains(ArgsConstants.CONCAT_MODE))
                    return Actions.Concat;
                
                throw new Exception("\"play\" or \"record\" or \"concat\" mode is expected");
            }
        }
    }
}