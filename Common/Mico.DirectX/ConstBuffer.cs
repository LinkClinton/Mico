using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Mico.DirectX
{
    /// <summary>
    /// ConstBuffer based on Buffer
    /// </summary>
    public class ConstBuffer : Buffer
    {

        /// <summary>
        /// Create ConstBuffer
        /// </summary>
        /// <param name="BufferData">It's size must div 16,and it can not be a array</param>
        public ConstBuffer(object BufferData)
        {
            GCHandle Handle = GCHandle.Alloc(BufferData, GCHandleType.Pinned);

            IntPtr buffer = Handle.AddrOfPinnedObject();

            BufferCreate(out source, buffer, Marshal.SizeOf(BufferData), Type.eConstBuffer, Direct3D.Core);

            Handle.Free();
        }

        ~ConstBuffer() => BufferDestory(source);

    }
}
