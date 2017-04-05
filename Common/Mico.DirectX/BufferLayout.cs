using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mico.DirectX
{
    /// <summary>
    /// VertexBuffer InputLayout
    /// </summary>
    public partial class BufferLayout
    {
        IntPtr source;

        /// <summary>
        /// Layout Element
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Element
        {
            /// <summary>
            /// A Element's size
            /// </summary>
            public ElementSize Size;

            /// <summary>
            /// A Element's Tag
            /// </summary>
            public string Tag;
        }


        /// <summary>
        /// Create BufferLayout
        /// </summary>
        /// <param name="Element">Describe how the VertexBuffer's Layout</param>
        public BufferLayout(Element[] Element)
            => BufferLayoutCreate(out source, Element, Element.Length, Direct3D.Core);
        

        ~BufferLayout() => BufferLayoutDestory(source);


        public static implicit operator IntPtr(BufferLayout bufferlayout)
            => bufferlayout.source;

    }
}
