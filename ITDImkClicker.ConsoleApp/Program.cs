using System;
using System.IO;
using System.Threading;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Extensions;
using ITDimkClicker.Implementations.Services;
using ITDimkClicker.Implementations.Utility;
using ITDimkClicker.Recorder.Data;
using ITDimkClicker.Recorder.Utility;

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
                _cancel = new CancellationTokenSource();

                _cancelHotkey = new CancellationHotkey(_cancel);
                _cancelHotkey.Register(ArgsVariables.BreakHotkey.Value, ArgsVariables.BreakModifier.Value);

                if (ArgsVariables.Mode == ArgsVariables.Actions.Play)
                    RunPlayer(_cancel.Token);

                else if (ArgsVariables.Mode == ArgsVariables.Actions.Record)
                    RunRecorder(_cancel.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }

        }

        static void RunRecorder(CancellationToken token)
        {
            IMacroRecorder recorder = new MacroRecorder(new RawInputReceiverWindow());
            IMacroFileManager fileManager = new MacroFileManager();

            var result = recorder.RunLoop(token);
            
            var output = File.OpenWrite(ArgsVariableGetter.Get(ArgsConstants.OUTPUT));
            fileManager.Write(result, output);
            output.Close();
        }

        static void RunPlayer(CancellationToken token)
        {
            IMacroFileManager fileManager = new MacroFileManager();
            var macros = fileManager.Read(File.OpenRead(ArgsVariables.InputFileName));

            var player = new MacroPlayer();
            player.RunLoop(macros, token);
        }
    }
}