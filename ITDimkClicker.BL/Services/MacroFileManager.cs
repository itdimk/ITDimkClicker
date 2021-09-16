using System.Drawing;
using System.IO;
using ITDimkClicker.Common.Data;
using ITDimkClicker.Common.Services;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using ITDimkClicker.Common.Extensions;

namespace ITDimkClicker.BL.Services
{
    public class MacroFileManager : IMacroFileManager
    {
        public void Write(Macro macros, Stream output)
        {
            output.Write(macros.Count);
            macros.ForEach(e => Write(e, output));
        }

        public Macro Read(Stream input)
        {
            int count = input.Read<int>();
            int startX = input.Read<int>();
            int startY = input.Read<int>();

            Macro result = new Macro();

            for (int i = 0; i < count; ++i)
                result.Add(ReadMacroEvent(input));
            return result;
        }

        private MacroEvent ReadMacroEvent(Stream input)
        {
            var timestamp = input.Read<long>();
            int macroEventType = input.Read<int>();

            if (macroEventType == 1)
            {
                var data = ReadRawInputData(input);
                return new InputMacroEvent(timestamp, data);
            }

            if (macroEventType == 2)
            {
                return new SetCursorPosMacroEvent(timestamp, input.Read<Point>());
            }

            return null;
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

        private void Write(MacroEvent e, Stream output)
        {
            output.Write<long>(e.Timestamp);

            if (e is InputMacroEvent inputEvent)
            {
                output.Write(1);
                Write(inputEvent.Data, output);
            }
            else if (e is SetCursorPosMacroEvent cursorEvent)
            {
                output.Write(2);
                output.Write(cursorEvent.Position);
            }
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