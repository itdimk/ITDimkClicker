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

namespace ITDImkClicker.ConsoleApp
{
    class Program
    {
        private static CancellationTokenSource _cancel;
        private static CancellationHotkey _cancelHotkey;

        static void Main()
        {
            // try
            // {
                if (ArgsVariables.Mode == ArgsConstants.MERGE_MODE)
                {
                    RunMerge();
                    return;
                }

                _cancel = new CancellationTokenSource();
                _cancelHotkey = new CancellationHotkey(_cancel);
                _cancelHotkey.Register(ArgsVariables.BreakHotkey, ArgsVariables.BreakModifier);

                if (ArgsVariables.Mode == ArgsConstants.PLAY_MODE)
                    RunPlayer(_cancel.Token);
                else if (ArgsVariables.Mode == ArgsConstants.RECORD_MODE)
                    RunRecorder(_cancel.Token);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.Message);
            //     Console.WriteLine();
            //     Console.WriteLine(e.StackTrace);
            //     Thread.Sleep(5000);
            // }
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

            using var output = File.OpenWrite(ArgsVariables.OutputFileName);
            io.Write( output, macro.ToArray());
        }

        static void RunRecorder(CancellationToken token)
        {
            IMacroRecorderApp recorderApp = new MacroRecorderApp(new RawInputReceiverWindow());
            IMacroIO io = new MacroIO();

            var macro = recorderApp.Run(token);
            using var output = File.OpenWrite(ArgsVariables.OutputFileName);
            io.Write(output, macro);
        }

        static void RunPlayer(CancellationToken token)
        {
            IMacroPlayerApp playerApp = new MacroPlayerApp();
            IMacroIO io = new MacroIO();

            using var input = File.OpenRead(ArgsVariables.InputFileName);
            var macros = io.ReadAll(input);

            playerApp.Run(macros, token);
        }
    }
}