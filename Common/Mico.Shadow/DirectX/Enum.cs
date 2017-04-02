using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX
{
    public partial class IShader
    {
        public enum Type
        {
            eVertexShader,
            ePixelShader
        }
    }

    public partial class IBuffer
    {
        public enum Type
        {
            eVertexBuffer,
            eIndexBuffer,
            eConstBuffer
        }
    }

    public partial class IBufferInput
    {
        public enum ElementSize
        {
            eFLOAT1,
            eFLOAT2,
            eFLOAT3,
            eFLOAT4
        }
    }

    public enum PrimitiveType
    {
        Unknown = 0,
        Point = 1,
        Line = 2,
        LineStrip = 3,
        Triangle = 4,
        TriangleStrip = 5,
    }
}
