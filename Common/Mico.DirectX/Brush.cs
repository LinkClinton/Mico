using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.DirectX
{
    /// <summary>
    /// Brush
    /// </summary>
    public partial class Brush
    {
        IntPtr source;

        /// <summary>
        /// Create Brush
        /// </summary>
        /// <param name="Color">Brush's color</param>
        public Brush(TVector4 Color)
            => BrushCreate(out source, Color, Direct3D.Core);

        /// <summary>
        /// Create Brush
        /// </summary>
        /// <param name="Red">Red</param>
        /// <param name="Green">Green</param>
        /// <param name="Blue">Blue</param>
        /// <param name="Alpha">Alpha</param>
        public Brush(float Red, float Green, float Blue, float Alpha = 1.0f)
            => BrushCreate(out source, new TVector4(Red, Green, Blue, Alpha), Direct3D.Core); 

        ~Brush() => BrushDestory(source);
        

        public static implicit operator IntPtr(Brush brush)
            => brush.source;
    }
}
