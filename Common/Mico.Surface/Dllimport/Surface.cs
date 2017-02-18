using System;
using System.Runtime.InteropServices;


namespace Mico.Surface
{
    delegate IntPtr WndProc(IntPtr Hwnd, Enum.Message message,
      IntPtr wParam, IntPtr lParam);

    public partial class Surface
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr DefWindowProc(IntPtr Hwnd, Enum.Message message,
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


        [DllImport("MicoCore.dll", CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateWindow([MarshalAs(UnmanagedType.LPStr)] String WindowTitle,
          [MarshalAs(UnmanagedType.LPStr)]  String IcoImage, int Width, int Height, WndProc proc);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetWindowText(IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPStr)] String WindowTitle);



    }
}
