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


namespace Mico.Collider.Sample
{
    public partial class Window
    {
        public delegate IntPtr WndProc(IntPtr Hwnd, uint message,
            IntPtr wParam, IntPtr lParam);

        IntPtr Hwnd;

        event WndProc WindowProc;

        int Width = 800;
        int Height = 600;

        public static int CubeCount { get => 50; }
        public static int XLimit { get => 100; }
        public static int YLimit { get => 100; }

        static Random random = new Random();

        public static int INT
        {
            get => random.Next(1, 70);
        }

        public static float FLOAT
        {
            get => (float)random.NextDouble();
        }

        Surface surface;
        Shader vertex;
        Shader pixel;

        FpsCounter fps;
        Fontface font;
        Brush brush;

        public Window()
        {
            WindowProc += Window_proc;
            Hwnd = CreateWindow("Mico", "", Width * (int)Direct3D.DpiScale,
                Height * (int)Direct3D.DpiScale, WindowProc);

            surface = new Surface(Hwnd);
            vertex = new VertexShader(@"ColliderVertexShader.hlsl", "main");
            pixel = new PixelShader(@"ColliderPixelShader.hlsl", "main");

            font = new Fontface("Consolas", 12 * Direct3D.DpiScale);
            brush = new Brush(0, 0, 0, 1);

            Micos.Add(fps = new FpsCounter());

            Direct3D.SetSurface(surface);
            Direct3D.SetShader(vertex);
            Direct3D.SetShader(pixel);
            Direct3D.FillMode = FillMode.Wireframe;
            Direct3D.CullMode = CullMode.CullNone;

            Micos.Camera = new Camera();
            Micos.Camera.Transform.Position = new Vector3(0, 0, -100);

            for (int i = 0; i < CubeCount; i++)
            {
                Cube cube = new Cube(10, 10, 10);
                cube.Transform.Position = new Vector3(INT, INT, 0);
                cube.Forward = Vector3.Normalize(new Vector3(INT, INT, 0));
                cube.RotateSpeed = new Vector3(FLOAT, FLOAT, 0);
                Micos.Add(cube);
            }
            
            Program.matrix.projection = (
                TMatrix.CreatePerspectiveFieldOfViewLH(
                    (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f));


        }

        public void OnRender()
        {
            Direct3D.Clear(new TVector4(1, 1, 1, 1));
            Micos.Exports();
            Direct3D.DrawText(fps.Fps.ToString(), new TVector2(0, 0), font, brush);
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
                Micos.Update();
                System.Threading.Thread.Sleep(1);
                OnRender();
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
