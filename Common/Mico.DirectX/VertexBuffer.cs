using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Mico.DirectX
{

    /// <summary>
    /// VertexBuffer based on Buffer
    /// </summary>
    public class VertexBuffer : Buffer
    {
        internal int eachsize;

        /// <summary>
        /// Create VertexBuffer
        /// </summary>
        /// <param name="BufferData">Vertex array</param>
        /// <param name="Length">array's length</param>
        /// <param name="ByteSize">element's size</param>
        public VertexBuffer(object BufferData, int Length, int ByteSize)
        {
            GCHandle Handle = GCHandle.Alloc(BufferData, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            BufferCreate(out source, buffer, Length * ByteSize, Type.eVertexBuffer, Direct3D.Core);

            Handle.Free();

            eachsize = ByteSize;
        }

        ~VertexBuffer() => BufferDestory(source);

      

    }
}
