using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public class TSize
    {
        float g_width;
        float g_height;


        public TSize(float width = 0, float height = 0)
        {
            g_width = width;
            g_height = height;
        }
        
        public float Width
        {
            set => g_width = value;
            get => g_width;
        }

        public float Height
        {
            set => g_height = value;
            get => g_height;
        }

    }
}
