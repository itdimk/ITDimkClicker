using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Services;
using ITDimkClicker.Recorder.Data;
using ITDimkClicker.Recorder.Utility;
using ITDimkClicker.RecorderApp.Utility;

namespace ITDimkClicker.RecorderApp
{
    class Program
    {
        private static string BreakHotkey => ArgsVariableGetter.GetVariable(ArgsConstants.BREAK_HOTKEY);
        private static string BreakModifier => ArgsVariableGetter.GetVariable(ArgsConstants.BREAK_MODIFIER);
        private static CancellationTokenSource _cancel;

        static void Main(string[] args)
        {
            _cancel = new CancellationTokenSource();
            IMacroRecorder recorder = new MacroRecorder(new RawInputReceiverWindow());
            IMacroFileManager fileManager = new MacroFileManager();

            RegisterCancellationHotkeys();
            var result = recorder.RunLoop(_cancel.Token);
            
            var output = File.OpenWrite(ArgsVariableGetter.GetVariable(ArgsConstants.OUTPUT));
            fileManager.Save(result, output);
            
            // -----------------==============----------------
            output.Close();
            var input = File.OpenRead(ArgsVariableGetter.GetVariable(ArgsConstants.OUTPUT));
            var macro = fileManager.Load(input);


        }

        static void RegisterCancellationHotkeys()
        {
            if (!Enum.TryParse(BreakHotkey, out Keys key))
                throw new Exception("Invalid break hotkey");

            if (!Enum.TryParse(BreakModifier, out ModifKeys modif))
                throw new Exception("Invalid break hotkey modifier");

            var hook = new KeyboardHook();
            hook.RegisterHotKey(modif, key);
            hook.KeyPressed += (o, e) => _cancel.Cancel();
        }
    }
}