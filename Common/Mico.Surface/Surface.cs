using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico
{
    public partial class Surface
    {
        private IntPtr WindowProc(IntPtr Hwnd, Enum.Message message,
            IntPtr wParam, IntPtr lParam)
        {
            g_message.Type = message;
            g_message.wParam = wParam;
            g_message.lParam = lParam;
            switch (message)
            {
                case Enum.Message.Destroy:
                    PostQuitMessage(0);
                    break;
                default:
                    return DefWindowProc(Hwnd, message, wParam, lParam);
            }
            return IntPtr.Zero;
        }

        protected void CreateWindow(string Title, string Ico, int Width, int Height)
        {
            g_width = Width;
            g_height = Height;

            g_origin_width = Width;
            g_origin_height = Height;

            g_title = Title;
            g_ico = Ico;

            g_message = new Message();

            g_hwnd = CreateWindow(Title, Ico, Width, Height, WindowProc);
        }

        public void Run()
        {
            while (g_message.Type != Enum.Message.Quit)
            {
                if (PeekMessage(out g_message, IntPtr.Zero, 0, 0, 1))
                {
                    TranslateMessage(ref g_message);
                    DispatchMessage(ref g_message);
                }
            }
        }



    }
}
