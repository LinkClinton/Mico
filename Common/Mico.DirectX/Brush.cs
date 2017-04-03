using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.DirectX
{
    public partial class Brush
    {
        IntPtr source;

        public Brush(TVector4 Color)
            => BrushCreate(out source, Color, Direct3D.Core);

        ~Brush() => BrushDestory(source);
        

        public static implicit operator IntPtr(Brush brush)
            => brush.source;
    }
}
