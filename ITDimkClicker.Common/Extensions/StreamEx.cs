using System.IO;
using System.Runtime.InteropServices;

namespace ITDimkClicker.Common.Extensions
{
    public static class StreamEx
    {
        public static unsafe T Read<T>(this Stream input)
            where T : unmanaged
        {
            int size = Marshal.SizeOf<T>();
            byte[] buffer = new byte[size];
            input.Read(buffer, 0, buffer.Length);

            fixed (byte* ptr = buffer)
                return *(T*) ptr;
        }

        public static unsafe void Write<T>(this Stream output, T data)
            where T : unmanaged
        {
            int size = Marshal.SizeOf<T>();
            byte[] buffer = new byte[size];

            for (int i = 0; i < buffer.Length; ++i)
                buffer[i] = *((byte*) &data + i);

            output.Write(buffer, 0, buffer.Length);
        }
    }
}