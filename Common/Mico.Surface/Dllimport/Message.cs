using System;
using System.Runtime.InteropServices;


namespace Mico
{
    partial struct Message
    {
        [DllImport("MicoCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LowWord(IntPtr Param);

        [DllImport("MicoCore.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HighWord(IntPtr Param);
    }
}
