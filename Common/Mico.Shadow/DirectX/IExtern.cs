using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Runtime.InteropServices;


using Mico.Math;

namespace Mico.Shadow.DirectX
{
    public delegate IntPtr WndProc(IntPtr Hwnd, uint message,
       IntPtr wParam, IntPtr lParam);

    static class IExtern
    {
        public const string DLLName = "Mico.Shadow.DirectX.dll";
    }


    public partial class IDevice
    {
        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceCreate(out IntPtr source,
            IntPtr hwnd, bool windowed = true);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceClear(IntPtr source, TVector4 color);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDevicePresent(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceRenderLine(IntPtr source,
            TVector2 start, TVector2 end, IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceRenderRect(IntPtr source,
            TRect rect, IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceFillRect(IntPtr source,
            TRect rect, IntPtr brush);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceRenderEllipse(IntPtr source,
            TVector2 center, TVector2 radius, IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceFillEllipse(IntPtr source,
            TVector2 center, TVector2 radius, IntPtr brush);

        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)] 
        static extern void IDirectXDeviceRenderText(IntPtr source,
            string text, TVector2 pos, IntPtr font, IntPtr brush);

        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IDirectXDeviceRenderBitmap(IntPtr source,
            TRect rect, IntPtr bitmap);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceTransform(IntPtr source,
            System.Numerics.Matrix3x2 matrix);


        [DllImport(IExtern.DLLName, CallingConvention = CallingConvention.StdCall,
              CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindow([MarshalAs(UnmanagedType.LPStr)] string Title,
             [MarshalAs(UnmanagedType.LPStr)]  string Ico, int Width, int Height, WndProc proc);
    }

    public partial class IBrush
    {
        [DllImport(IExtern.DLLName)]
        static extern void IDirectXBrushCreate(out IntPtr source,
            IntPtr device, TVector4 color);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXBrushDestory(IntPtr source);
    }

    public partial class IFont
    {
        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)] 
        static extern void IDirectXFontCreate(out IntPtr source,
            IntPtr device, string font, float size, int weight = 400);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXFontDestory(IntPtr source);
    }

    public partial class IBitmap
    {
        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IDirectXBitmapCreate(out IntPtr source,
            IntPtr device, string filename, ref float width, ref float height);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXBitmapDestory(IntPtr source);
    }

    public partial class IShader
    {
        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IDirectXShaderCreate(out IntPtr source,
            string filename, string entrypoint, Type type);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXShaderDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXShaderCompile(IntPtr source);
    }


}
