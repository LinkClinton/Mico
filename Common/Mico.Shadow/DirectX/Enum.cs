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
}
