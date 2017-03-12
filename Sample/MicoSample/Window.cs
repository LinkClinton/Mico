using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Mico.Shadow.DirectX;

namespace MicoSample
{
    public partial class Window
    {
        IntPtr Hwnd;

        IDevice device;
        IBitmap bitmap;

      
  
        public Window()
        {
            Hwnd = IDevice.CreateWindow("Mico", "", 800, 600, Window_proc);
            device = new IDevice(Hwnd);
            bitmap = new IBitmap(device, @"草图.png");
        }

        public void OnRender()
        {
            device.Clear(new Mico.Math.Vector4(1, 1, 1, 1));
            device.RenderBitmap(new Mico.Math.Rect(0, 0, 800, 600), bitmap);
            device.RenderLine(new Mico.Math.Vector2(0, 0), new Mico.Math.Vector2(100, 100),
                new IBrush(device, new Mico.Math.Vector4(0, 0, 0, 1)));
            device.RenderText("Text Test", new Mico.Math.Vector2(100, 100),
                new IFont(device, "Consolas", 12), new IBrush(device, new Mico.Math.Vector4(0, 0, 0, 1)));
            device.Present();
        }

        public void Run()
        {
            Message message = new Message();
            while (message.Type != MessageType.Quit)
            {
                if (PeekMessage(out message, IntPtr.Zero, 0, 0, 1))
                {
                    TranslateMessage(ref message);
                    DispatchMessage(ref message);
                }else
                {
                    OnRender();
                }
            }
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr DefWindowProc(IntPtr Hwnd, uint message,
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


        protected IntPtr Window_proc(IntPtr Hwnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            MessageType type = (MessageType)message;
            switch (type)
            {
                case MessageType.Destroy:
                    PostQuitMessage(0);
                    break;
                case MessageType.SizeChange:
                    break;
                case MessageType.Quit:
                    break;
                case MessageType.KeyDown:
                    break;
                case MessageType.KeyUp:
                    break;
                case MessageType.MouseMove:
                    break;
                case MessageType.LeftButtonDown:
                    break;
                case MessageType.LeftButtonUp:
                    break;
                case MessageType.RightButtonDown:
                    break;
                case MessageType.RightButtonUp:
                    break;
                case MessageType.MiddleButtonDown:
                    break;
                case MessageType.MiddleButtonUp:
                    break;
                case MessageType.MouseWheelMove:
                    break;
                default:
                    return DefWindowProc(Hwnd, message, wParam, lParam);
            }

            return IntPtr.Zero;
        }
    }
}
