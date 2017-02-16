using System;
using System.Runtime.InteropServices;

namespace Mico
{
    public static partial class Input
    {
        [DllImport("MicoCore.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IsKeyDown(Keycode keycode);


    }
}
