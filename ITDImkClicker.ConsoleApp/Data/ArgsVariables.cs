using System;
using System.Windows.Forms;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;
using ITDimkClicker.Recorder.Data;

namespace ITDImkClicker.ConsoleApp.Data
{
    static class ArgsVariables
    {
        public static string OutputFileName => ArgsVariableGetter.Get(ArgsConstants.OUTPUT, true);
        public static string InputFileName => ArgsVariableGetter.Get(ArgsConstants.INPUT, true);
        public static string[] InputFileNames => ArgsVariableGetter.GetMany(ArgsConstants.INPUT, true);
        public static string Mode => ArgsVariableGetter.GetValueAt(1);

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

    }
}