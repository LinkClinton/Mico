using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shapes
{
    public class Transform
    {
        Vector3 g_pos = new Vector3();
        Vector3 g_forward = new Vector3();
        Vector3 g_scale = new Vector3(1, 1, 1);

        float g_speed = 0;
        float g_angle = 0;

        public Vector3 Position
        {
            get { return g_pos; }
            set { g_pos = value; }
        }

        public Vector3 Forward
        {
            get { return g_forward; }
            set
            {
                g_forward = (value - Position).Normalize;
            }
        }

        public float Speed
        {
            get { return g_speed; }
            set { g_speed = value; }
        }

        public Vector3 Center
        {
            get { return g_pos; }
        }

        public Vector3 Scale
        {
            get { return g_scale; }
            set { g_scale = value; }
        }
    }
}
