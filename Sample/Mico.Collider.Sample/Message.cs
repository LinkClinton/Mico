using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Collider.Sample
{
    enum MessageType
    {
        Destroy = 0x0002,
        SizeChange = 0x0005,
        Quit = 0x0012,
        KeyDown = 0x0100,
        KeyUp = 0x0101,
        MouseMove = 0x0200,
        LeftButtonDown = 0x0201,
        LeftButtonUp = 0x0202,
        RightButtonDown = 0x0204,
        RightButtonUp = 0x0205,
        MiddleButtonDown = 0x0207,
        MiddleButtonUp = 0x0208,
        MouseWheelMove = 0x020A
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Message
    {
        public IntPtr Hwnd;
        public MessageType Type;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public int x;
        public int y;

        public static int LowWord(IntPtr number)
            => (int)number & 0xffff;

        public static int HighWord(IntPtr number)
            => ((int)number >> 16) & 0xffff; 
    }
}
