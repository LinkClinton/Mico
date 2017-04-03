using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mico.DirectX
{
    public abstract partial class Buffer
    {
        protected IntPtr source;

        protected Type type;

        public Type BufferType { get => type; }

        public void Update(object BufferData)
        {
            GCHandle Handle = GCHandle.Alloc(BufferData, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            Handle.Free();

            BufferUpdate(source, buffer, Direct3D.Core);
        }

        public static implicit operator IntPtr(Buffer buffer)
            => buffer.source;
    }
}
