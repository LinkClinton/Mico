using System;
using System.Runtime.InteropServices;

namespace Mico.Window
{
    using MessageType = Enum.Message;

    delegate IntPtr WndProc(IntPtr Hwnd, MessageType message,
      IntPtr wParam, IntPtr lParam);

    public partial class Window
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr DefWindowProc(IntPtr Hwnd, MessageType message,
        IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool PeekMessage(out Message message, IntPtr hwnd,
            int wMSGfilterMin, int wMsgFilterMax, int wRemoveMsg);

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage(ref Message message);

        [DllImport("user32.dll")]
        internal static extern bool DispatchMessage(ref Message message);

        [DllImport("user32.dll")]
        internal static extern void PostQuitMessage(int exitCode);
    }
}
