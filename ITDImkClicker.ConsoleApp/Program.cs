using System;
using System.IO;
using System.Threading;
using ITDimkClicker.BL.Services;
using ITDimkClicker.BL.Utility;
using ITDimkClicker.Common.Services;
using ITDimkClicker.Recorder.Data;

namespace ITDImkClicker.ConsoleApp
{
    class Program
    {
        private static CancellationTokenSource _cancel;
        private static CancellationHotkey _cancelHotkey;

        static int Main()
        {
            try
            {
                if (ArgsVariables.Mode == ArgsVariables.Actions.Concat)
                    RunConcat();
                else
                {
                    _cancel = new CancellationTokenSource();
                    _cancelHotkey = new CancellationHotkey(_cancel);
                    _cancelHotkey.Register(ArgsVariables.BreakHotkey, ArgsVariables.BreakModifier);

                    if (ArgsVariables.Mode == ArgsVariables.Actions.Play)
                        RunPlayer(_cancel.Token);
                    else if (ArgsVariables.Mode == ArgsVariables.Actions.Record)
                        RunRecorder(_cancel.Token);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e.StackTrace);
                Thread.Sleep(2000);
                return -1;
            }

            return 0;
        }

        static void RunRecorder(CancellationToken token)
        {
            IMacroRecorder recorder = new MacroRecorder(new RawInputReceiverWindow());
            IMacroFileManager fileManager = new MacroFileManager();

            var macros = recorder.RunLoop(token);
            using var output = File.OpenWrite(ArgsVariables.OutputFileName);
            fileManager.Write(macros, output);
        }

        static void RunPlayer(CancellationToken token)
        {
            IMacroPlayer player = new MacroPlayer();
            IMacroFileManager fileManager = new MacroFileManager();

            using var input = File.OpenRead(ArgsVariables.InputFileName);
            var macros = fileManager.Read(input);

            player.RunLoop(macros, token);
        }

        static void RunConcat()
        {
            IMacroFileManager fileManager = new MacroFileManager();

            using var input = File.OpenRead(ArgsVariables.InputFileName);
            using var output = File.Open(ArgsVariables.OutputFileName, FileMode.Open, FileAccess.ReadWrite);

            var inputMacro = fileManager.Read(input);
            var outputMacro = fileManager.Read(output);

            output.Position = 0;
            outputMacro.Concat(inputMacro);
            fileManager.Write(outputMacro, output);
        }
    }
}