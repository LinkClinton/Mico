using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TVector4
    {
        float m_x;
        float m_y;
        float m_z;
        float m_w;

        public TVector4(float x, float y, float z, float w)
        {
            m_x = x;
            m_y = y;
            m_z = z;
            m_w = w;
        }

        public float Red { get => m_x; }
        public float Green { get => m_y; }
        public float Blue { get => m_z; }
        public float Alpha { get => m_w; }

        public float X { get => m_x; }
        public float Y { get => m_y; }
        public float Z { get => m_z; }
        public float W { get => m_w; }

        public static implicit operator TVector4(System.Numerics.Vector4 vec)
            => new TVector4(vec.X, vec.Y, vec.Z, vec.W); 

        public static implicit operator System.Numerics.Vector4(TVector4 vec)
            => new System.Numerics.Vector4(vec.m_x, vec.m_y, vec.m_z, vec.m_w); 
    }
}
