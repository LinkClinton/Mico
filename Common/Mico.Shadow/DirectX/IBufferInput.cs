using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Shadow.DirectX
{
    public partial class IBufferInput
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Element
        {
            public string Tag;
            public ElementSize Size;
        }

        IntPtr source;

        public IBufferInput(IDevice device, Element[] element)
            => IDirectXBufferInputCreate(out source, device, element, element.Length); 

        ~IBufferInput()
            => IDirectXBufferInputDestory(source);


        public static implicit operator IntPtr(IBufferInput bufferinput)
            => bufferinput.source;

    }
}
