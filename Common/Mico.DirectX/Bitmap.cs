using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    public partial class Bitmap
    {
        IntPtr source;

        float width;
        float height;

        public Bitmap(string Filename)
            => BitmapCreate(out source, Filename, ref width, ref height, Direct3D.Core);

        ~Bitmap() => BitmapDestory(source);

        public float Width { get => width; }

        public float Height { get => height; }


        public static implicit operator IntPtr(Bitmap bitmap)
            => bitmap.source;
    }
}
