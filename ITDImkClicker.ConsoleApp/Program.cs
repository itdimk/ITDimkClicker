using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using ITDImkClicker.ConsoleApp.Data;
using ITDimkClicker.Recorder.Data;
using WindowsInput;

namespace ITDImkClicker.ConsoleApp
{
    class Program
    {
        private static CancellationTokenSource _cancel;
        private static CancellationHotkey _cancelHotkey;

        static void Main()
        {
            try
            {
                if (ArgsVariables.Mode == ArgsConstants.MERGE_MODE)
                {
                    RunMerge();
                    return;
                }


                if (ArgsVariables.Mode == ArgsConstants.PLAY_MODE)
                {
                    _cancel = new CancellationTokenSource();
                    _cancelHotkey = new CancellationHotkey(_cancel);
                    _cancelHotkey.Register(ArgsVariables.BreakHotkey, ArgsVariables.BreakModifier);
                    RunPlayer(_cancel.Token);
                }
                else if (ArgsVariables.Mode == ArgsConstants.RECORD_MODE)
                {
                    RunRecorder();
                }
            }
            catch (Exception e)
            {
                string text = e.Message + "\n" + e.StackTrace + "\n" + e;
                File.WriteAllText("log.txt", text);
                Console.WriteLine("App crashed. See log.txt for details");
                Thread.Sleep(3000);
            }
        }

        static void RunMerge()
        {
            var macro = new List<Macro>();
            IMacroIO io = new MacroIO();
            string[] files = ArgsVariables.InputFileNames;

            foreach (string file in files)
            {
                using var input = File.OpenRead(file);
                macro.AddRange(io.ReadAll(input));
            }

            float speed = ArgsVariables.Speed;

            if (ArgsVariables.Speed > 0)
                foreach (var m in macro)
                foreach (var e in m)
                    e.Timestamp = (long) Math.Round(e.Timestamp / speed);

            using var output = File.Open(ArgsVariables.OutputFileName, FileMode.OpenOrCreate);
            io.Write(output, macro.ToArray());
        }

        static void RunRecorder()
        {
            using IMacroRecorderApp recorderApp = new MacroRecorderApp(new RawInputReceiverWindow());
            IMacroIO io = new MacroIO();

            var macro = recorderApp.Run(ArgsVariables.BreakHotkey, ArgsVariables.BreakModifier);
            if (File.Exists(ArgsVariables.OutputFileName))
                File.Delete(ArgsVariables.OutputFileName);

            using var output = File.OpenWrite(ArgsVariables.OutputFileName);
            io.Write(output, macro);
        }

        static void RunPlayer(CancellationToken token)
        {
            var simulator = new InputSimulator();
            using MacroPlayer playerApp = new MacroPlayerApp(new MacroEventPlayer(simulator));
            IMacroIO io = new MacroIO();

            using var input = File.OpenRead(ArgsVariables.InputFileName);
            var macros = io.ReadAll(input);

            playerApp.Run(macros, token, ArgsVariables.Speed);
        }
    }
}