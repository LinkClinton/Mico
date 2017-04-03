using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Mico.DirectX
{
    public class VertexBuffer : Buffer
    {
        internal int eachsize;

        public VertexBuffer(object BufferData, int Length, int ByteSize)
        {
            GCHandle Handle = GCHandle.Alloc(BufferData, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            BufferCreate(out source, buffer, Length * ByteSize, Type.eVertexBuffer, Direct3D.Core);

            Handle.Free();

            type = Type.eVertexBuffer;

            eachsize = ByteSize;
        }

        ~VertexBuffer() => BufferDestory(source);

      

    }
}
