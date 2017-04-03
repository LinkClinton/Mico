using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Mico.DirectX
{
    public class ConstBuffer : Buffer
    {
        public ConstBuffer(object BufferData)
        {
            GCHandle Handle = GCHandle.Alloc(BufferData, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            BufferCreate(out source, buffer, Marshal.SizeOf(BufferData), Type.eConstBuffer, Direct3D.Core);

            Handle.Free();

            type = Type.eConstBuffer;
        }

        ~ConstBuffer() => BufferDestory(source);

    }
}
