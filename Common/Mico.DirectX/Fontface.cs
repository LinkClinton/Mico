using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    public partial class Fontface
    {
        IntPtr source;

        public Fontface(string Fontface, float Size, int Weight = 400)
            => FontfaceCreate(out source, Fontface, Size, Weight, Direct3D.Core);

        ~Fontface() => FontfaceDestory(source);


        public static implicit operator IntPtr(Fontface fontface)
            => fontface.source;
    }
}
