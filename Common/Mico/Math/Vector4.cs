using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Math
{
    public class Vector4
    {
        float g_x;
        float g_y;
        float g_z;
        float g_w;

        public override string ToString()
        {
            return "x=" + g_x + " y=" + g_y + " z=" + g_z + " w=" + g_w;
        }

        public Vector4 (float x,float y,float z,float w)
        {
            g_x = x;
            g_y = y;
            g_z = z;
            g_w = w;
        }

        public float Red
        {
            get { return g_x; }
        }

        public float Green
        {
            get { return g_y; }
        }

        public float Blue
        {
            get { return g_z; }
        }

        public float Alpha
        {
            get { return g_w; }
        }


        public float x
        {
            get { return g_x; }
        }

        public float y
        {
            get { return g_y; }
        }

        public float z
        {
            get { return g_z; }
        }

        public float w
        {
            get { return g_w; }
        }
    }
}
