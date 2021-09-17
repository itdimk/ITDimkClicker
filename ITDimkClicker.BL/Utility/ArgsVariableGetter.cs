using System;
using System.Collections.Generic;

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

        public static string[] GetMany(string variableName, bool throwIfEmpty)
        {
            var result = new List<string>();
            for (int i = 0; i < args.Length; ++i)
                if (args[i] == variableName)
                    result.Add(args[i + 1]);
            return result.ToArray();
        }

        public static string GetValueAt(int index)
        {
            if (index >= 0 && index < args.Length)
                return args[index];
            throw new Exception($"Can't get command value at index{index}");
        }

        public static string[] GetArgs() => args;
    }
}