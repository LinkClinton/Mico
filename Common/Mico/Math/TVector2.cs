using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TVector2
    {
        float m_x;
        float m_y;

        public TVector2(float x = 0, float y = 0)
        {
            m_x = x;
            m_y = y;
        }


        public float X { get => m_x; }
        public float Y { get => m_y; }


        public static implicit operator TVector2(System.Numerics.Vector2 vec)
            => new TVector2(vec.X, vec.Y); 

        public static implicit operator System.Numerics.Vector2(TVector2 vec)
            => new System.Numerics.Vector2(vec.m_x, vec.m_y); 

    }
}
