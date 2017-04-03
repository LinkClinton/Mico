using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.DirectX
{
    public static partial class Direct3D
    {
        static IntPtr source;

        static Direct3D()
        {
            ManagerCreate(out source);
        }

        public static void SetSurface(Surface Surface)
            => ManagerSetSurface(source, Surface);

        public static void Clear(TVector4 Color)
            => ManagerClear(source, Color);

        public static void Present()
            => ManagerPresent(source);

        public static void DrawLine(TVector2 Start, TVector2 End,
            Brush Brush, float Width = 1.0f)
            => ManagerDrawLine(source, Start, End, Brush, Width);

        public static void DrawRect(TRect Rect, Brush Brush, float Width = 1.0f)
            => ManagerDrawRect(source, Rect, Brush, Width);

        public static void FillRect(TRect Rect, Brush Brush)
            => ManagerFillRect(source, Rect, Brush);

        public static void DrawEllipse(TVector2 Center, TVector2 Radius, Brush Brush,
            float Width = 1.0f)
            => ManagerDrawEllipse(source, Center, Radius, Brush, Width);

        public static void FillEllipse(TVector2 Center, TVector2 Radius, Brush Brush)
            => ManagerFillEllipse(source, Center, Radius, Brush);

        public static void DrawText(string Text, TVector2 Position, Fontface Fontface,
            Brush Brush)
            => ManagerDrawText(source, Text, Position, Fontface, Brush);

        public static void DrawBitmap(TRect Rect, Bitmap Bitmap)
            => ManagerDrawBitmap(source, Rect, Bitmap);

        public static void SetBuffer(Buffer Buffer, int BufferID, Shader.Type To)
            => ManagerSetBuffer(source, Buffer, BufferID, To);

        public static void SetBuffer(Buffer Buffer)
        {
            switch (Buffer)
            {
                case VertexBuffer vertex:
                    ManagerSetVertexBuffer(source, vertex, vertex.eachsize);
                    break;
                case IndexBuffer index:
                    ManagerSetIndexBuffer(source, index);
                    break;
                default:
                    throw new Exception("Do not support Buffer Type");
            }
        }

        public static void SetShader(Shader shader)
        {
            ManagerSetShader(source, shader);
        }

        public static void SetBufferToVertexShader(Buffer Buffer, int BufferID)
            => ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.eVertexShader);

        public static void SetBufferToPixelShader(Buffer Buffer, int BufferID)
            => ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.ePixelShader);



        public static IntPtr Core
        {
            get => source;
        }
    }
}
