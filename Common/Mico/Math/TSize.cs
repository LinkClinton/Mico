using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TSize
    {
        float m_with;
        float m_height;


        public TSize(float width = 0, float height = 0)
        {
            m_with = width;
            m_height = height;
        }
        
        public float Width
        {
            set => m_with = value;
            get => m_with;
        }

        public float Height
        {
            set => m_height = value;
            get => m_height;
        }

    }
}
