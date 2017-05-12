using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.DirectX
{
    /// <summary>
    /// Core class
    /// </summary>
    public static partial class Direct3D
    {
        static IntPtr source;

        static FillMode fillmode = FillMode.Solid;
        static CullMode cullmode = CullMode.CullNone;

        static Direct3D()
        {
            ManagerCreate(out source);
        }

        /// <summary>
        /// Set Surface
        /// </summary>
        /// <param name="Surface"></param>
        public static void SetSurface(Surface Surface)
            => ManagerSetSurface(source, Surface);

        /// <summary>
        /// Clear Surface
        /// </summary>
        /// <param name="Color">Color</param>
        public static void Clear()
            => ManagerClear(source);

        /// <summary>
        /// Present Surface
        /// </summary>
        public static void Present()
            => ManagerPresent(source);

        /// <summary>
        /// Draw Line
        /// </summary>
        /// <param name="Start">Line's start point</param>
        /// <param name="End">Line's end point</param>
        /// <param name="Brush">Brush</param>
        /// <param name="Width">Line's width</param>
        public static void DrawLine(TVector2 Start, TVector2 End,
            Brush Brush, float Width = 1.0f)
            => ManagerDrawLine(source, Start, End, Brush, Width);

        /// <summary>
        /// Draw Rectangle
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Brush">Brush</param>
        /// <param name="Width">Line's width</param>
        public static void DrawRect(TRect Rect, Brush Brush, float Width = 1.0f)
            => ManagerDrawRect(source, Rect, Brush, Width);

        /// <summary>
        /// Fill Rectangle
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Brush">Brush</param>
        public static void FillRect(TRect Rect, Brush Brush)
            => ManagerFillRect(source, Rect, Brush);

        /// <summary>
        /// Draw Ellipse
        /// </summary>
        /// <param name="Center">Ellipse's center position</param>
        /// <param name="Radius">Ellipse's radius</param>
        /// <param name="Brush">Brush</param>
        /// <param name="Width">Line's width</param>
        public static void DrawEllipse(TVector2 Center, TVector2 Radius, Brush Brush,
            float Width = 1.0f)
            => ManagerDrawEllipse(source, Center, Radius, Brush, Width);

        /// <summary>
        /// Fill Ellipse
        /// </summary>
        /// <param name="Center">Ellipse's center position</param>
        /// <param name="Radius">Ellipse's radius</param>
        /// <param name="Brush">Brush</param>
        public static void FillEllipse(TVector2 Center, TVector2 Radius, Brush Brush)
            => ManagerFillEllipse(source, Center, Radius, Brush);

        /// <summary>
        /// Draw Text
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="Position">Text start position</param>
        /// <param name="Fontface">Font</param>
        /// <param name="Brush">Brush</param>
        public static void DrawText(string Text, TVector2 Position, Fontface Fontface,
            Brush Brush)
            => ManagerDrawText(source, Text, Position, Fontface, Brush);

        /// <summary>
        /// Draw Bitmap
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Bitmap">Bitmap</param>
        public static void DrawBitmap(TRect Rect, Bitmap Bitmap)
            => ManagerDrawBitmap(source, Rect, Bitmap);

        /// <summary>
        /// Set Buffer To Shader
        /// </summary>
        /// <param name="Buffer">Buffer</param>
        /// <param name="BufferID">BufferID in Shader</param>
        /// <param name="which">Shader(must be set)</param>
        public static void SetBuffer(Buffer Buffer, int BufferID, Shader which)
        {
            switch (which)
            {
                case VertexShader shader:
                    ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.eVertexShader);
                    break;
                case PixelShader shader:
                    ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.ePixelShader);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Set Buffer 
        /// </summary>
        /// <param name="Buffer">Vertex or Index</param>
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

        /// <summary>
        /// Set Shader
        /// </summary>
        /// <param name="shader">Vertex or Pixel</param>
        public static void SetShader(Shader shader)
            => ManagerSetShader(source, shader);

        /// <summary>
        /// Set Buffer To Vertex Shader
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="BufferID">Buffer ID in Shader</param>
        public static void SetBufferToVertexShader(Buffer Buffer, int BufferID)
            => ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.eVertexShader);

        /// <summary>
        /// Set Buffer To Pixel Shader
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="BufferID">Buffer ID in Shader</param>
        public static void SetBufferToPixelShader(Buffer Buffer, int BufferID)
            => ManagerSetBuffer(source, Buffer, BufferID, Shader.Type.ePixelShader);

        /// <summary>
        /// Set VertexBuffer InputLayout
        /// </summary>
        /// <param name="BufferLayout">InputLayout</param>
        public static void SetBufferLayout(BufferLayout BufferLayout)
            => ManagerSetBufferLayout(source, BufferLayout);

        /// <summary>
        /// Draw Buffer
        /// </summary>
        /// <param name="VertexCount">VertexCount</param>
        /// <param name="StartLocation">Start position in VertexBuffer</param>
        /// <param name="Type"></param>
        public static void Draw(int VertexCount, int StartLocation = 0,
            PrimitiveType Type = PrimitiveType.Triangle)
            => ManagerDraw(source, VertexCount, StartLocation, Type);

        /// <summary>
        /// Draw Buffer by Index
        /// </summary>
        /// <param name="IndexCount">IndexCount</param>
        /// <param name="StartLocation">Start position in IndexBuffer</param>
        /// <param name="VertexCount">Start position in VertexBuffer</param>
        /// <param name="Type"></param>
        public static void DrawIndexed(int IndexCount, int StartLocation = 0,
            int VertexCount = 0, PrimitiveType Type = PrimitiveType.Triangle)
            => ManagerDrawIndexed(source, IndexCount, StartLocation, VertexCount, Type);

        /// <summary>
        /// FillMode
        /// </summary>
        public static FillMode FillMode
        {
            get => fillmode;
            set
            {
                fillmode = value;
                ManagerSetFillMode(source, value);
            }
        }

        /// <summary>
        /// CullMode
        /// </summary>
        public static CullMode CullMode
        {
            get => cullmode;
            set
            {
                cullmode = value;
                ManagerSetCullMode(source, value);
            }
        }

        /// <summary>
        /// Current Dpi
        /// </summary>
        public static TVector2 CurrentDpi
        {
            get
            {
                ManagerGetDpi(source, out float dpiX, out float dpiY);
                return new TVector2(dpiX, dpiY);
            }
        }

        /// <summary>
        /// (DpiX+DpiY)/192
        /// </summary>
        public static float DpiScale
        {
            get => (CurrentDpi.X + CurrentDpi.Y) / 192.0f;
        }

        public static IntPtr Core
        {
            get => source;
        }
    }
}
