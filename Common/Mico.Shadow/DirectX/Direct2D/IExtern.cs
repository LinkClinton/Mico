using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Shadow.DirectX.Direct2D
{

    static class IExtern
    {
        public const string DLLName = "Mico.Shadow.DirectX.dll";
    }


    public delegate IntPtr WndProc(IntPtr Hwnd, uint message,
       IntPtr wParam, IntPtr lParam);

    public partial class IFactory
    {
        [DllImport(IExtern.DLLName)]
        static extern void IFactoryCreate(out IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IFactoryDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IFactoryGetDesktopDpi(IntPtr source, out float dpiX, out float dpiY);

        [DllImport(IExtern.DLLName, CallingConvention = CallingConvention.StdCall,
               CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindow([MarshalAs(UnmanagedType.LPStr)] string Title,
              [MarshalAs(UnmanagedType.LPStr)]  string Ico, int Width, int Height, WndProc proc);

    }

    public partial class IRenderTarget
    {

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetCreate(out IntPtr source, IntPtr factory, IntPtr hwnd);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetClear(IntPtr source, float r, float g, float b, float a = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetBeginDraw(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetEndDraw(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetDrawLine(IntPtr source, float x1, float y1, float x2, float y2,
            IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetDrawRectangle(IntPtr source, float left, float top, float right, float bottom,
            IntPtr brush, float width = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetFillRectangle(IntPtr source, float left, float top, float right, float bottom,
            IntPtr brush);

        [DllImport(IExtern.DLLName,CharSet = CharSet.Auto)]
        static extern void IRenderTargetDrawText(IntPtr source, float x, float y, string text,
            IntPtr font, IntPtr brush);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetDrawBitmap(IntPtr source, float x, float y,
            IntPtr bitmap);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetRotate(IntPtr source, float x, float y, float angle);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetTranslate(IntPtr source, float x, float y);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetScale(IntPtr source, float x, float y, float scale_x, float scale_y);

        [DllImport(IExtern.DLLName)]
        static extern void IRenderTargetClearTransform(IntPtr source);
    }

    public partial class IBrush
    {
        [DllImport(IExtern.DLLName)]
        static extern void IBrushCreate(out IntPtr source, IntPtr target, float r, float g, float b, float a = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IBrushDestory(IntPtr source);
    }

    public partial class IFont
    {
        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IFontCreate(out IntPtr soure, IntPtr target,
            string fontface, float size, int weight = 400);

        [DllImport(IExtern.DLLName)]
        static extern void IFontDestory(IntPtr source);
    }


    public partial class IBitmap
    {
        [DllImport(IExtern.DLLName, CharSet = CharSet.Auto)]
        static extern void IBitmapCreate(out IntPtr source, IntPtr target, string filename);

        [DllImport(IExtern.DLLName)]
        static extern void IBitmapDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern float IBitmapGetWidth(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern float IBitmapGetHeight(IntPtr source);


    }



}
