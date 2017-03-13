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
        float g_left;
        float g_top;
        float g_right;
        float g_bottom;

        public TRect(float left = 0, float top = 0, float right = 0, float bottom = 0)
        {
            g_left = left;
            g_top = top;
            g_right = right;
            g_bottom = bottom;
        }

        public float Left
        {
            get => g_left;
            set => g_left = value; 
        }

        public float Right
        {
            get => g_right;
            set => g_right = value; 
        }

        public float Top
        {
            get => g_top;
            set => g_top = value;
        }
        
        public float Bottom
        {
            get => g_bottom;
            set => g_bottom = value; 
        }


    }
}
