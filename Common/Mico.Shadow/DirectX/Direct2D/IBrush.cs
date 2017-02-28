using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX.Direct2D
{
    public partial class IBrush
    {
        IntPtr source;

        public IBrush(IRenderTarget target,Vector4 color)
        {
            IBrushCreate(out source, target, color.x, color.y, color.z, color.w);
        }

        ~IBrush()
        {
            IBrushDestory(source);
        }

        public static implicit operator IntPtr(IBrush brush)
        {
            return brush.source;
        }
    }
}
