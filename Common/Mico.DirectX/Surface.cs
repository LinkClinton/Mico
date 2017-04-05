using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    /// <summary>
    /// Surface to Present
    /// </summary>
    public partial class Surface
    {
        IntPtr source;

        /// <summary>
        /// Create Surface
        /// </summary>
        /// <param name="Hwnd">Window Hwnd</param>
        /// <param name="Windowed">Is Windowed</param>
        public Surface(IntPtr Hwnd, bool Windowed = true)
            => SurfaceCreate(out source, Hwnd, Windowed, Direct3D.Core);

        ~Surface() => SurfaceDestory(source);





        public static implicit operator IntPtr(Surface surface)
            => surface.source;
    }
}
