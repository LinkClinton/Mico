using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Math;
using Mico.Objects;


using Mico.DirectX;


namespace Mico.Test.Sample
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
        Camera camera;
        Shader vertexshader;
        Shader pixelshader;
        IObject cube;
        BufferLayout layout;
        ConstBuffer MatrixBuffer;

        DirectX.TransformMatrix matrix;


        public void Initialize()
        {
            BufferLayout.Element[] element = new BufferLayout.Element[2];

            element[0] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFloat3,
                Tag = "POSITION"
            };
            element[1] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFlaot4,
                Tag = "COLOR"
            };

            layout = new BufferLayout(element);

            cube = IObject.CreateBox(3, 3, 3);
        }

        public void SetObject()
        {
            Direct3D.SetBufferToVertexShader(MatrixBuffer, 0);
            Direct3D.SetBufferLayout(layout);
            Direct3D.SetBuffer(cube.vertexbuffer);
            Direct3D.SetBuffer(cube.indexbuffer);
            Direct3D.FillMode = FillMode.Solid;
        }

        public Window()
        {
            WindowProc += Window_proc;
            Hwnd = CreateWindow("Mico", "", Width, Height, WindowProc);
            surface = new Surface(Hwnd);
            vertexshader = new VertexShader(@"C:\Users\linka\Documents\Visual Studio 2017\Projects\Mico\Sample\Mico.Test.Sample\VertexShader.hlsl", "main");
            pixelshader = new PixelShader(@"C:\Users\linka\Documents\Visual Studio 2017\Projects\Mico\Sample\Mico.Test.Sample\PixelShader.hlsl", "main");
            Direct3D.SetSurface(surface);
            Direct3D.SetShader(vertexshader);
            Direct3D.SetShader(pixelshader);

            matrix = new DirectX.TransformMatrix();

            camera = new Camera()
            {
                LookAt = new Vector3(0, 0, 0),
                Up = new Vector3(0, 1, 0)
            };

            camera.Transform.Position = new Vector3(3, 4, -10);

            matrix.view = Matrix4x4.Transpose(camera);
            matrix.world = Matrix4x4.Identity;
            matrix.projection = Matrix4x4.Transpose(Matrix4x4.CreatePerspectiveFieldOfView((float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 1000.0f));

            MatrixBuffer = new ConstBuffer(matrix);

            Initialize();
            SetObject();
          
        }

        public void OnRender()
        {
            Direct3D.Clear(new TVector4(1, 1, 1, 1));
            Direct3D.DrawIndexed(36);
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
