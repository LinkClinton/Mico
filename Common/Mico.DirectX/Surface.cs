using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    public partial class Surface
    {
        IntPtr source;

        public Surface(IntPtr Hwnd, bool Windowed = true)
            => SurfaceCreate(out source, Hwnd, Windowed, Direct3D.Core);

        ~Surface() => SurfaceDestory(source);





        public static implicit operator IntPtr(Surface surface)
            => surface.source;
    }
}
