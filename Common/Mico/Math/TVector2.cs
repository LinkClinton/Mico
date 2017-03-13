using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{

    [StructLayout(LayoutKind.Sequential)]
    public class TVector2
    {
        float g_x;
        float g_y;

        public override string ToString()
            => "x=" + g_x + ",y=" + g_y;


        public TVector2(float x = 0, float y = 0)
        {
            g_x = x;
            g_y = y;
        }


        public float x { get => g_x; }
        public float y { get => g_y; }


        public static implicit operator TVector2(System.Numerics.Vector2 vec)
            => new TVector2(vec.X, vec.Y);

        public static implicit operator System.Numerics.Vector2(TVector2 vec)
            => new System.Numerics.Vector2(vec.x, vec.y); 

    }
}
