using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX
{
    public partial class IBrush
    {
        IntPtr source = IntPtr.Zero;
        
        public IBrush(IDevice device,Vector4 color)
            => IDirectXBrushCreate(out source, device, color);

        ~IBrush() => IDirectXBrushDestory(source);



        public static implicit operator IntPtr(IBrush brush)
            =>  brush.source;
    }
}
