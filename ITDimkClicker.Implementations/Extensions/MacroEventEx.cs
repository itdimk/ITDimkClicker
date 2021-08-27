using System;
using System.IO;
using ITDimkClicker.Abstractions.Data;
using Linearstar.Windows.RawInput;

namespace ITDimkClicker.Implementations.Extensions
{
    public static class MacroEventEx
    {
        private enum DeviceTypes
        {
            Keyboard = 1,
            Mouse = 2
        }

        public static void WriteToStream(this MacroEvent e, Stream output)
        {
            byte[] timestamp = BitConverter.GetBytes(e.Timestamp);
            output.Write(timestamp, 0, timestamp.Length);

            switch (e.Data)
            {
                case RawInputMouseData mouseData:
                    output.WriteByte((byte)DeviceTypes.Mouse);
                    mouseData.WriteToStream(output);
                    break;
                case RawInputKeyboardData keyboardData:
                    output.WriteByte((byte)DeviceTypes.Keyboard);
                    keyboardData.WriteToStream(output);
                    break;
            }
        }

        public static MacroEvent ReadFromStream(Stream source)
        {
            byte[] timestampBytes = new byte[sizeof(long)];
            source.Read(timestampBytes, 0, timestampBytes.Length);
            long timestamp = BitConverter.ToInt64(timestampBytes);

            DeviceTypes type = (DeviceTypes) source.ReadByte();

            switch (type)
            {
                case DeviceTypes.Mouse:
                    var mData = RawInputMouseDataEx.ReadFromStream(source);
                    return new MacroEvent(timestamp, mData);
                
                case DeviceTypes.Keyboard:
                    var kData = RawInputKeyboardDataEx.ReadFromStream(source);
                    return new MacroEvent(timestamp, kData);
                default:
                    throw new ArgumentException("Wrong device type");
            }
            
        }
    }
}