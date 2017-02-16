using System;

namespace Mico.Window
{
    using MessageType = Enum.Message;

    partial class Message
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
