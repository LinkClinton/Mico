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
        Vector2 g_pos = new Vector2();
        Vector2 g_forward = new Vector2();
        Vector2 g_scale = new Vector2(1, 1);

        float g_speed = 0;
        float g_angle = 0;

        public Vector2 Position
        {
            get { return g_pos; }
            set { g_pos = value; }
        }

        public Vector2 Forward
        {
            get { return g_forward; }
            set
            {
                g_forward = (value - Position).Normalize;
                g_angle = 360.0f - (float)System.Math.Atan2(g_forward.y, g_forward.x) * 180 / (float)System.Math.PI;
            }
        }

        public float Angle
        {
            get { return g_angle; }
        }

        public float Speed
        {
            get { return g_speed; }
            set { g_speed = value; }
        }

        public Vector2 Center
        {
            get { return g_pos; }
        }

        public Vector2 Scale
        {
            get { return g_scale; }
            set { g_scale = value; }
        }
    }
}
