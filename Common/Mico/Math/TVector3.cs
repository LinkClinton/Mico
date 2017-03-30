using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public class TVector3
    {
        float g_x;
        float g_y;
        float g_z;


        public override string ToString()
            => "x = " + g_x + ", y = " + g_y + ", z = " + g_z;


        public TVector3(float x = 0, float y = 0, float z = 0)
        {
            g_x = x;
            g_y = y;
            g_z = z;
        }

        public float x { get => g_x; }
        public float y { get => g_y; }
        public float z { get => g_z; }

        public static implicit operator TVector3(System.Numerics.Vector3 vec)
            => new TVector3(vec.X, vec.Y, vec.Z);

        public static implicit operator System.Numerics.Vector3(TVector3 vec)
            => new System.Numerics.Vector3(vec.x, vec.y, vec.z);
        

        public static TVector3 Forward
        {
            get => new TVector3(0, 0, 1);
        }

        public static TVector3 Left
        {
            get => new TVector3(-1, 0, 0);
        }

        public static TVector3 Right
        {
            get => new TVector3(1, 0, 0);
        }

        public static TVector3 Up
        {
            get => new TVector3(0, 1, 0);
        }

        public static TVector3 Down
        {
            get => new TVector3(0, -1, 0);
        }

    }
}
