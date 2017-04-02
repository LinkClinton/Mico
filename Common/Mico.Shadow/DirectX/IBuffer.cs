using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX
{
    public partial class IBuffer
    {
        IntPtr source;

        public IBuffer(IDevice device, IntPtr data, int size, Type type)
            => IDirectXBufferCreate(out source, device, data, size, type);

        ~IBuffer() => IDirectXBufferDestory(source);

        public void Update(object data)
            => IDirectXBufferUpdate(source, data);


        public static implicit operator IntPtr(IBuffer buffer)
            => buffer.source;
    }
}
