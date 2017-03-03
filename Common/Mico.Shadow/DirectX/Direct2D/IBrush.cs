using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX.Direct2D
{
    /// <summary>
    /// Brush Source
    /// </summary>
    public partial class IBrush
    {
        IntPtr source;

        public IBrush(IRenderTarget target, Vector4 color)
        {
            IBrushCreate(out source, target, color.Red, color.Green, color.Blue, color.Alpha);
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
