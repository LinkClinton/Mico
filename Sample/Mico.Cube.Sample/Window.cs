using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Math;
using Mico.Shapes;
using Mico.Objects;
using Mico.DirectX;


namespace Mico.Cube.Sample
{
    public partial class Window
    {
        public delegate IntPtr WndProc(IntPtr Hwnd, uint message,
            IntPtr wParam, IntPtr lParam);

        IntPtr Hwnd;

        event WndProc WindowProc;

        int Width = 800;
        int Height = 600;

        Surface surface;
        Shader vertex;
        Shader pixel;

        public Window()
        {
            WindowProc += Window_proc;
            Hwnd = CreateWindow("Mico", "", Width * (int)Direct3D.DpiScale,
                Height * (int)Direct3D.DpiScale, WindowProc);

            surface = new Surface(Hwnd);
            vertex = new VertexShader(@"..\..\Sample\Mico.Cube.Sample\VertexShader.hlsl", "main");
            pixel = new PixelShader(@"..\..\Sample\Mico.Cube.Sample\PixelShader.hlsl", "main");


            Direct3D.SetSurface(surface);
            Direct3D.SetShader(vertex);
            Direct3D.SetShader(pixel);
            Direct3D.FillMode = FillMode.Solid;
            Direct3D.CullMode = CullMode.CullNone;
            Micos.Camera = new Camera();

            Micos.Add(IObject.CreateBox(3, 3, 3));

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);

            Micos.Camera.Project =
                 TMatrix.CreatePerspectiveFieldOfViewLH(
                     (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);


        }

        public void OnRender()
        {
            Direct3D.Clear(new TVector4(1, 1, 1, 1));
            Micos.Exports();
            Direct3D.Present();
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
                System.Threading.Thread.Sleep(1);
                Micos.Update();
            }
        }

        [DllImport("Mico.DirectX.Core.dll", CallingConvention = CallingConvention.StdCall,
             CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindow([MarshalAs(UnmanagedType.LPStr)] string Title,
            [MarshalAs(UnmanagedType.LPStr)]  string Ico, int Width, int Height, WndProc proc);

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
