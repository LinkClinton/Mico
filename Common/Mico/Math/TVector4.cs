using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public class TVector4
    {
        float g_x;
        float g_y;
        float g_z;
        float g_w;

        public override string ToString()
            => "x=" + g_x + " y=" + g_y + " z=" + g_z + " w=" + g_w;


        public TVector4(float x, float y, float z, float w)
        {
            g_x = x;
            g_y = y;
            g_z = z;
            g_w = w;
        }

        public float Red { get => g_x; }
        public float Green { get => g_y; }
        public float Blue { get => g_z; }
        public float Alpha { get => g_w; }

        public float x { get => g_x; }
        public float y { get => g_y; }
        public float z { get => g_z; }
        public float w { get => g_w; }

        public static implicit operator TVector4(System.Numerics.Vector4 vec)
            => new TVector4(vec.X, vec.Y, vec.Z, vec.W); 
    }
}
