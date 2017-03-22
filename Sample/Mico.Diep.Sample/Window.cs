using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Math;
using Mico.Objects;
using Mico.Shadow.DirectX;

using Mico.Diep.Sample.GameObject;

namespace Mico.Diep.Sample
{
    public partial class Window
    {
        IntPtr Hwnd;

        IDevice device;

        event WndProc WindowProc;


        FpsCounter fps;

        public void InitializeWorld()
        {
            new BoxTank()
            {
                IsPlayer = true,
                Speed = 100,
                Radius = new TVector2(30, 30)
            };

            fps = new FpsCounter();

            World.Micos.Add(fps);
        }

        public Window()
        {
            WindowProc += Window_proc;
            Hwnd = IDevice.CreateWindow("Mico", "", 800, 600, WindowProc);
            device = new IDevice(Hwnd);

            GameResource.Brush.Initialize(device);

            InitializeWorld();
        }

        public void OnRender()
        { 

            device.Clear(new TVector4(1, 1, 1, 1));
            World.Micos.Exports(device);
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
                }
                OnRender();
                World.Micos.Update();
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

        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int keyCode);

        protected static IntPtr Window_proc(IntPtr Hwnd, uint message, IntPtr wParam, IntPtr lParam)
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
                    GameInput.Input.MousePos = new TVector2(Message.LowWord(lParam),
                        Message.HighWord(lParam));
                    break;
                case MessageType.LeftButtonDown:
                    GameInput.Input.LeftMouse = true;
                    break;
                case MessageType.LeftButtonUp:
                    GameInput.Input.LeftMouse = false;
                    break;
                case MessageType.RightButtonDown:
                    GameInput.Input.RightMouse = true;
                    break;
                case MessageType.RightButtonUp:
                    GameInput.Input.RightMouse = false;
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
