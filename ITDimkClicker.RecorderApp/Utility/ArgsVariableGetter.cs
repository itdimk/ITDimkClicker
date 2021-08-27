using System;

namespace ITDimkClicker.Recorder.Utility
{
    public static class ArgsVariableGetter
    {
        private static string[] args = Environment.GetCommandLineArgs();

        public static string GetVariable(string variableName)
        {
            int index = Array.IndexOf(args, variableName);

            if (index >= 0 && index < args.Length - 1)
                return args[index + 1];
            return null;
        }
    }
}