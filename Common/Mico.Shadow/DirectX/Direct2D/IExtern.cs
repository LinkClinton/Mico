﻿using System;
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


    public partial class IFactory
    {
        [DllImport(IExtern.DLLName)]
        static extern void IFactoryCreate(out IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IFactoryDestory(IntPtr source);

        [DllImport(IExtern.DLLName)]
        static extern void IFactoryGetDesktopDpi(IntPtr source, out float dpiX, out float dpiY);
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
    }

    public partial class IBrush
    {
        [DllImport(IExtern.DLLName)]
        static extern void IBrushCreate(out IntPtr source, IntPtr target, float r, float g, float b, float a = 1.0f);

        [DllImport(IExtern.DLLName)]
        static extern void IBrushDestory(IntPtr source);
    }

}