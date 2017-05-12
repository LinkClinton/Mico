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


        /// <summary>
        /// Resize Buffer
        /// </summary>
        /// <param name="Width">new Width</param>
        /// <param name="Height">new Height</param>
        public void Resize(float Width, float Height)
            => SurfaceResize(source, Width, Height);


        public static implicit operator IntPtr(Surface surface)
            => surface.source;
    }
}
