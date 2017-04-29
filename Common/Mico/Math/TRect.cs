using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public class TRect
    {
        float m_left;
        float m_top;
        float m_right;
        float m_bottom;

        public TRect(float left = 0, float top = 0, float right = 0, float bottom = 0)
        {
            m_left = left;
            m_top = top;
            m_right = right;
            m_bottom = bottom;
        }

        public float Left
        {
            get => m_left;
            set => m_left = value; 
        }

        public float Right
        {
            get => m_right;
            set => m_right = value; 
        }

        public float Top
        {
            get => m_top;
            set => m_top = value;
        }
        
        public float Bottom
        {
            get => m_bottom;
            set => m_bottom = value; 
        }


    }
}
