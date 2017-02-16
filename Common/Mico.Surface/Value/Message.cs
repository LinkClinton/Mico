using System;
using System.Runtime.InteropServices;

namespace Mico
{
    using MessageType = Enum.Message;

    [StructLayout(LayoutKind.Sequential)]
    partial struct Message
    {
        public IntPtr Hwnd;
        public MessageType Type;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public int x;
        public int y;
    }
}
