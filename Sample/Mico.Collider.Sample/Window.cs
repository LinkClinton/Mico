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

using Presenter;


namespace Mico.Collider.Sample
{
    public partial class Window
    {
        public static TVector2 MousePos = new TVector2(0, 0);

        IntPtr Hwnd;

        event APILibrary.Win32.Internal.WndProc WindowProc;

        int Width = 800;
        int Height = 600;

        public static int CubeCount { get => 50; }
        public static int XLimit { get => 100; }
        public static int YLimit { get => 100; }
        public static int ZLimit { get => 100; }

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

            APILibrary.Win32.AppInfo appinfo = new APILibrary.Win32.AppInfo()
            {
                style = (uint)(APILibrary.Win32.AppInfoStyle.CS_HREDRAW | APILibrary.Win32.AppInfoStyle.CS_VREDRAW),
                lpfnWndProc = WindowProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = APILibrary.Win32.Internal.GetModuleHandle(null),
                hIcon = IntPtr.Zero,
                hbrBackground = IntPtr.Zero,
                hCursor = APILibrary.Win32.Internal.LoadCursor(IntPtr.Zero, (uint)APILibrary.Win32.CursorType.IDC_ARROW),
                lpszClassName = "Mico",
                lpszMenuName = null
            };

            APILibrary.Win32.Internal.RegisterAppinfo(ref appinfo);

            Hwnd = APILibrary.Win32.Internal.CreateWindowEx(0, "Mico", "Mico",
                (uint)APILibrary.Win32.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, (int)(Width * Manager.DpiX / 96),
                (int)(Height * Manager.DpiY / 96), IntPtr.Zero, IntPtr.Zero, appinfo.hInstance, IntPtr.Zero);

            APILibrary.Win32.Internal.ShowWindow(Hwnd, (int)APILibrary.Win32.ShowWindowStyles.SW_NORMAL);

            surface = new Surface(Hwnd, true);
            vertex = new VertexShader(@"ColliderVertexShader.hlsl", "main");
            pixel = new PixelShader(@"ColliderPixelShader.hlsl", "main");

            font = new Fontface("Consolas", (int)(12 * Manager.AppScale));
            brush = new Brush((0, 0, 0, 1));

            Micos.Add(fps = new FpsCounter());


            Manager.Surface = surface;
            Manager.VertexShader = vertex as VertexShader;
            Manager.PixelShader = pixel as PixelShader;

            Manager.FillMode = FillMode.Wireframe;
            Manager.CullMode = CullMode.CullNone;

            Micos.Camera = new Camera();
            Micos.Camera.Transform.Position = new Vector3(0, 0, -100);

            for (int i = 0; i < CubeCount; i++)
            {
                Cube cube = new Cube(10, 10, 10);
                cube.Transform.Position = new Vector3(INT, INT, INT);
                cube.Forward = Vector3.Normalize(new Vector3(INT, INT, 0));
                cube.RotateSpeed = new Vector3(FLOAT, FLOAT, 0);
                Micos.Add(cube);
            }


            Micos.Camera.Project = TMatrix.CreatePerspectiveFieldOfViewLH(
                    (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);


        }

        public void OnMouseDown()
        {
            Cube cube = Micos.Pick(Camera.NdcX(MousePos.X, Width * Manager.AppScale),
                Camera.NdcY(MousePos.Y, Height * Manager.AppScale)) as Cube;

            if (cube is null) return;
            Micos.Remove(cube);
        }

        public void OnUpdate()
        {
           
        }

        public void OnRender()
        {
            Manager.ClearObject();

            Micos.Exports();

            Manager.PutObject(fps.Fps.ToString(), (0, 0), brush, font);
            Manager.PutObject("Click cube to destory it!", (0, font.Size), brush, font);

            Manager.FlushObject();
        }

        public void Run()
        {
            APILibrary.Win32.Message message = new APILibrary.Win32.Message();
            while (message.type != (uint)APILibrary.Win32.WinMsg.WM_QUIT)
            {
                if (APILibrary.Win32.Internal.PeekMessage(out message, IntPtr.Zero, 0, 0, 1))
                {
                    APILibrary.Win32.Internal.TranslateMessage(ref message);
                    APILibrary.Win32.Internal.DispatchMessage(ref message);
                }
                OnRender();
                System.Threading.Thread.Sleep(1);
                Micos.Update();
            }
        }

        protected IntPtr Window_proc(IntPtr Hwnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            APILibrary.Win32.WinMsg type = (APILibrary.Win32.WinMsg)message;
            switch (type)
            {
                case APILibrary.Win32.WinMsg.WM_DESTROY:
                    APILibrary.Win32.Internal.PostQuitMessage(0);
                    break;
                case APILibrary.Win32.WinMsg.WM_SIZE:
                    Width = (int)(APILibrary.Win32.Message.LowWord(lParam) / Manager.AppScale);
                    Height = (int)(APILibrary.Win32.Message.HighWord(lParam) / Manager.AppScale);
                    break;
                case APILibrary.Win32.WinMsg.WM_MOUSEMOVE:
                    MousePos = new TVector2(APILibrary.Win32.Message.LowWord(lParam), 
                        APILibrary.Win32.Message.HighWord(lParam));
                    break;
                case APILibrary.Win32.WinMsg.WM_LBUTTONDOWN:
                    OnMouseDown();
                    break;
                default:
                    return APILibrary.Win32.Internal.DefWindowProc(Hwnd, message, wParam, lParam);
            }

            return IntPtr.Zero;
        }
    }
}
