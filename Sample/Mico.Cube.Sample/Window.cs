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

namespace Mico.Cube.Sample
{
    public partial class Window
    {
        IntPtr Hwnd;

        event APILibrary.Win32.Internal.WndProc WindowProc;

        int Width = 800;
        int Height = 600;

        Surface surface;
        Shader vertex;
        Shader pixel;
        Texture texture;

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
            vertex = new VertexShader(@"..\..\Sample\Mico.Cube.Sample\VertexShader.hlsl", "main");
            pixel = new PixelShader(@"..\..\Sample\Mico.Cube.Sample\PixelShader.hlsl", "main");

            texture = new Texture(@"..\..\Sample\Mico.Cube.Sample\Dream.png");

            Manager.Surface = surface;

            Manager.VertexShader = vertex as VertexShader;
            Manager.PixelShader = pixel as PixelShader;

            Manager.FillMode = FillMode.Solid;
            Manager.CullMode = CullMode.CullBack;

            Manager.ShaderResource[(pixel, 0)] = texture;

            Micos.Camera = new Camera();

            Micos.Add(IObject.CreateBox(4, 4, 4));

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;

            Micos.Camera.Project =
                 TMatrix.CreatePerspectiveFieldOfViewLH(
                     (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);


        }

        public void OnRender()
        {
            Manager.ClearObject();
            Micos.Exports();
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
                    surface?.Reset(APILibrary.Win32.Message.LowWord(lParam),
                        APILibrary.Win32.Message.HighWord(lParam));
                    break;
                default:
                    return APILibrary.Win32.Internal.DefWindowProc(Hwnd, message, wParam, lParam);
            }
            return IntPtr.Zero;
        }
    }
}
