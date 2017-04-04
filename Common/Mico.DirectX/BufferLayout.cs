using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mico.DirectX
{
    public partial class BufferLayout
    {
        IntPtr source;

        [StructLayout(LayoutKind.Sequential)]
        public struct Element
        {
            public ElementSize Size;
            public string Tag;
        }


        public BufferLayout(Element[] Element)
            => BufferLayoutCreate(out source, Element, Element.Length, Direct3D.Core);
        

        ~BufferLayout() => BufferLayoutDestory(source);


        public static implicit operator IntPtr(BufferLayout bufferlayout)
            => bufferlayout.source;

    }
}
