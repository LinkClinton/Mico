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
        static extern void IDirectXDeviceClear(IntPtr source, Vector4 color);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDevicePresent(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IDirectXDeviceRenderLine(IntPtr source,
            Vector2 start, Vector2 end, IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)] 
        static extern void IDirectXDeviceRenderText(IntPtr source,
            string text, Vector2 pos, IntPtr font, IntPtr brush);

        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IDirectXDeviceRenderBitmap(IntPtr source,
            Rect rect, IntPtr bitmap);


        [DllImport(IExtern.DLLName, CallingConvention = CallingConvention.StdCall,
              CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindow([MarshalAs(UnmanagedType.LPStr)] string Title,
             [MarshalAs(UnmanagedType.LPStr)]  string Ico, int Width, int Height, WndProc proc);
    }

    public partial class IBrush
    {
        [DllImport(IExtern.DLLName)]
        static extern void IDirectXBrushCreate(out IntPtr source,
            IntPtr device, Vector4 color);

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


}
