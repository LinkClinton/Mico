using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    /// <summary>
    /// Bitmap
    /// </summary>
    public partial class Bitmap
    {
        IntPtr source;

        float width;
        float height;

        /// <summary>
        /// Create Bitmap
        /// </summary>
        /// <param name="Filename">FileName</param>
        public Bitmap(string Filename)
            => BitmapCreate(out source, Filename, ref width, ref height, Direct3D.Core);

        ~Bitmap() => BitmapDestory(source);

        /// <summary>
        /// Bitmap's Width
        /// </summary>
        public float Width { get => width; }

        /// <summary>
        /// Bitmap's Height
        /// </summary>
        public float Height { get => height; }


        public static implicit operator IntPtr(Bitmap bitmap)
            => bitmap.source;
    }
}
