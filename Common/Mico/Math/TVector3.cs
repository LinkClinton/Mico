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
        float m_x;
        float m_y;
        float m_z;

        public TVector3(float x = 0, float y = 0, float z = 0)
        {
            m_x = x;
            m_y = y;
            m_z = z;
        }

        public float X { get => m_x; }
        public float Y { get => m_y; }
        public float Z { get => m_z; }

        public static implicit operator TVector3(System.Numerics.Vector3 vec)
            => new TVector3(vec.X, vec.Y, vec.Z);

        public static implicit operator System.Numerics.Vector3(TVector3 vec)
            => new System.Numerics.Vector3(vec.m_x, vec.m_y, vec.m_z);

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
