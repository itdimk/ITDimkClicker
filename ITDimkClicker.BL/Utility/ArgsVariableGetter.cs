using System;

namespace ITDimkClicker.BL.Utility
{
    public static class ArgsVariableGetter
    {
        private static string[] args = Environment.GetCommandLineArgs();

        public static string Get(string variableName, bool throwIfNull)
        {
            int index = Array.IndexOf(args, variableName);

            if (index >= 0 && index < args.Length - 1)
                return args[index + 1];

            if (throwIfNull)
                throw new Exception($"Can't get value of command line argument {variableName}");
            
            return null;
        }
    }
}