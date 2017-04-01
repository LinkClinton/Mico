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


namespace Mico.Test.Sample
{
    public partial class Window
    {
        IntPtr Hwnd;

        IDevice device;

        event WndProc WindowProc;

        int Width = 800;
        int Height = 600;
      
        FpsCounter fps;

        IShader shader;
        IBufferInput bufferinput;

        public void InitializeWorld()
        {
            device = new IDevice(Hwnd);

            shader = new IShader(@"C:\Users\linka\Documents\Visual Studio 2017\Projects\Mico\Sample\Mico.Test.Sample\VertexShader.hlsl",
                "main", IShader.Type.eVertexShader);
            shader.Compile();

            device.SetShader(shader);

            IBufferInput.Element[] element = new IBufferInput.Element[2];

            element[0].Tag = "POSITION";
            element[0].Size = IBufferInput.ElementSize.eFLOAT3;

            element[1].Tag = "COLOR";
            element[1].Size = IBufferInput.ElementSize.eFLOAT4;

            bufferinput = new IBufferInput(device, element);
        }

        public Window()
        {
            WindowProc += Window_proc;
            Hwnd = IDevice.CreateWindow("Mico", "", Width, Height, WindowProc);
            

       

            InitializeWorld();
        }

        public void OnRender()
        { 

            device.Clear(new TVector4(1, 1, 1, 1));
          
      
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
