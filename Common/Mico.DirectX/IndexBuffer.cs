using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Mico.DirectX
{
    /// <summary>
    /// IndexBuffer based on Buffer
    /// </summary>
    public class IndexBuffer : Buffer
    {
        /// <summary>
        /// Create IndexBuffer
        /// </summary>
        /// <param name="Index">Index array</param>
        public IndexBuffer(uint[] Index)
        {
            GCHandle Handle = GCHandle.Alloc(Index, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            BufferCreate(out source, buffer, Index.Length * sizeof(uint), Type.eIndexBuffer, Direct3D.Core);

            Handle.Free();
        }

        ~IndexBuffer() => BufferDestory(source);

    }
}
