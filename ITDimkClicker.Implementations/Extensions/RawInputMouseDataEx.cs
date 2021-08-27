using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace ITDimkClicker.Implementations.Extensions
{
    public static class RawInputMouseDataEx
    {
        public static unsafe RawInputMouseData ReadFromStream(Stream source)
        {
            var headerSize = Marshal.SizeOf<RawInputHeader>();
            var mouseSize = Marshal.SizeOf<RawMouse>();
            
            byte[] data = new byte[headerSize + mouseSize];
            source.Read(data, 0, data.Length);

            fixed (byte* dataPtr = data)
            {
                var header = *(RawInputHeader*) dataPtr;
                var mouse = *(RawMouse*) (dataPtr + headerSize);
                return new RawInputMouseData(header, mouse);
            }
        }

        public static void WriteToStream(this RawInputMouseData data, Stream destination)
        {
            byte[] buffer = data.ToStructure();
            destination.Write(buffer, 0, buffer.Length);
        }
    }
}