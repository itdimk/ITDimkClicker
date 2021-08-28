using System;
using System.IO;
using ITDimkClicker.Abstractions.Data;
using ITDimkClicker.Abstractions.Services;
using ITDimkClicker.Implementations.Extensions;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Services
{
    public class MacroFileManager : IMacroFileManager
    {
        public void Write(Macro macros, Stream output)
        {
            output.Write(macros.Count);
            output.Write(macros.StartX);
            output.Write(macros.StartY);
            macros.ForEach(e => Write(e, output));
        }

        public Macro Read(Stream input)
        {
            int count = input.Read<int>();
            int startX = input.Read<int>();
            int startY = input.Read<int>();
            
            Macro result = new Macro(startX, startY);
            
            for(int i = 0; i < count; ++i)
                result.Add(ReadMacroEvent(input));
            return result;
        }

        private MacroEvent ReadMacroEvent(Stream input)
        {
            var timestamp = input.Read<long>();
            var data = ReadRawInputData(input);
            return new MacroEvent(timestamp, data);
        }
        
        private void Write(MacroEvent e, Stream output)
        {
            output.Write<long>(e.Timestamp);
            Write(e.Data, output);
        }

        private RawInputData ReadRawInputData(Stream input)
        {
            int id = input.Read<int>();
            var header = input.Read<RawInputHeader>();

            return id switch
            {
                1 => new RawInputMouseData(header, input.Read<RawMouse>()),
                2 => new RawInputKeyboardData(header, input.Read<RawKeyboard>()),
                _ => null
            };
        }

        private void Write(RawInputData data, Stream output)
        {
            byte[] buffer = data.ToStructure();

            int id = data switch
            {
                RawInputMouseData => 1,
                RawInputKeyboardData => 2,
                _ => -1
            };

            output.Write(id);
            output.Write(buffer, 0, buffer.Length);
        }
    }
}