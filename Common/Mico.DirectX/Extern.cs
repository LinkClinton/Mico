using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Mico.Math;

namespace Mico.DirectX
{
    static class Extern
    {
        public const string DLLName = "Mico.DirectX.Core.dll";
    }

    public enum PrimitiveType
    {
        UNK,
        Point,
        Line,
        LineStrip,
        Triangle,
        TriangleStrip,
    };

    public enum CullMode
    {
        CullNone = 1,
        CullFront = 2,
        CullBack = 3
    }

    public enum FillMode
    {
        Wireframe = 2,
        Solid = 3
    }

    public static partial class Direct3D
    {
        [DllImport(Extern.DLLName)]
        static extern void ManagerCreate(out IntPtr source);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDestory(IntPtr source);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetSurface(IntPtr source, IntPtr surface);

        [DllImport(Extern.DLLName)]
        static extern void ManagerClear(IntPtr source, TVector4 color);

        [DllImport(Extern.DLLName)]
        static extern void ManagerPresent(IntPtr source);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDrawLine(IntPtr source, TVector2 start, TVector2 end,
            IntPtr brush, float width);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDrawRect(IntPtr source, TRect rect, IntPtr brush,
            float width);

        [DllImport(Extern.DLLName)]
        static extern void ManagerFillRect(IntPtr source, TRect rect, IntPtr brush);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDrawEllipse(IntPtr source, TVector2 center, TVector2 radius,
            IntPtr brush, float width);

        [DllImport(Extern.DLLName)]
        static extern void ManagerFillEllipse(IntPtr source, TVector2 center, TVector2 radius,
            IntPtr brush);

        [DllImport(Extern.DLLName, CharSet = CharSet.Auto)]
        static extern void ManagerDrawText(IntPtr source, string text, TVector2 pos,
            IntPtr fontface, IntPtr brush);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDrawBitmap(IntPtr source, TRect rect, IntPtr bitmap);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetShader(IntPtr source, IntPtr shader);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetBuffer(IntPtr source, IntPtr buffer, int bufferid, Shader.Type type);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetVertexBuffer(IntPtr source, IntPtr buffer, int eachsize);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetIndexBuffer(IntPtr source, IntPtr buffer);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetBufferLayout(IntPtr source, IntPtr bufferlayout);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDraw(IntPtr source, int vertexcount, int startlocation, PrimitiveType type);

        [DllImport(Extern.DLLName)]
        static extern void ManagerDrawIndexed(IntPtr source, int indexcount, int startlocation, int vertexlocation,
            PrimitiveType type);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetCullMode(IntPtr source, CullMode mode);

        [DllImport(Extern.DLLName)]
        static extern void ManagerSetFillMode(IntPtr source, FillMode mode);

    }

    public partial class Surface
    {
        [DllImport(Extern.DLLName)]
        static extern void SurfaceCreate(out IntPtr source, IntPtr hwnd, 
            bool windowed, IntPtr manager);

        [DllImport(Extern.DLLName)]
        static extern void SurfaceDestory(IntPtr source);
    }

    public partial class Brush
    {
        [DllImport(Extern.DLLName)]
        static extern void BrushCreate(out IntPtr source, TVector4 color, IntPtr manager);

        [DllImport(Extern.DLLName)]
        static extern void BrushDestory(IntPtr source);
    }

    public partial class Fontface
    {
        [DllImport(Extern.DLLName, CharSet = CharSet.Auto)]
        static extern void FontfaceCreate(out IntPtr source, string fontface, float size,
            int weight, IntPtr manager);

        [DllImport(Extern.DLLName)]
        static extern void FontfaceDestory(IntPtr source);
    }

    public partial class Bitmap
    {
        [DllImport(Extern.DLLName, CharSet = CharSet.Auto)]
        static extern void BitmapCreate(out IntPtr source, string filename, ref float width,
            ref float height, IntPtr manager);

        [DllImport(Extern.DLLName)]
        static extern void BitmapDestory(IntPtr source);
    }

    public partial class Shader
    {
        [DllImport(Extern.DLLName, CharSet = CharSet.Auto)]
        protected static extern void ShaderCreate(out IntPtr source, string filename, string entrypoint,
            Type type, bool IsCompile, IntPtr manager);

        [DllImport(Extern.DLLName)]
        protected static extern void ShaderDestory(IntPtr source);


        public enum Type
        {
            eVertexShader,
            ePixelShader
        }
    }

    public partial class Buffer
    {
        [DllImport(Extern.DLLName)]
        protected static extern void BufferCreate(out IntPtr source, IntPtr buffer, int size,
            Type type, IntPtr manager);

        [DllImport(Extern.DLLName)]
        protected static extern void BufferDestory(IntPtr source);

        [DllImport(Extern.DLLName)]
        protected static extern void BufferUpdate(IntPtr source, IntPtr buffer, IntPtr manager);

        public enum Type
        {
            eVertexBuffer,
            eIndexBuffer,
            eConstBuffer
        }

    }


    public partial class BufferLayout
    {
        [DllImport(Extern.DLLName, CharSet = CharSet.Auto)]
        static extern void BufferLayoutCreate(out IntPtr source, Element[] element, int elementsize,
            IntPtr manager);

        [DllImport(Extern.DLLName)]
        static extern void BufferLayoutDestory(IntPtr source);

        public enum ElementSize
        {
            eFloat1,
            eFloat2,
            eFloat3,
            eFlaot4
        }
    }



}
