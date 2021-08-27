using System.IO;
using System.Runtime.InteropServices;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Extensions
{
    public static class RawInputKeyboardDataEx
    {
        public static unsafe RawInputKeyboardData ReadFromStream(Stream source)
        {
            var headerSize = Marshal.SizeOf<RawInputHeader>();
            var mouseSize = Marshal.SizeOf<RawKeyboard>();
            
            byte[] data = new byte[headerSize + mouseSize];
            source.Read(data, 0, data.Length);

            fixed (byte* dataPtr = data)
            {
                var header = *(RawInputHeader*) dataPtr;
                var mouse = *(RawKeyboard*) (dataPtr + headerSize);
                return new RawInputKeyboardData(header, mouse);
            }
        }
        
        public static void WriteToStream(this RawInputKeyboardData data, Stream output)
        {
            byte[] buffer = data.ToStructure();
            output.Write(buffer, 0, buffer.Length);
        }
    }
}