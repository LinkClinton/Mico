using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico.Math;



namespace Mico.Shapes
{
    public class Transform
    {
        Vector3 g_pos = new Vector3();
        Vector3 g_forward = new Vector3();
        Vector3 g_scale = new Vector3(1, 1, 1);

        float g_speed = 0;

        float g_forward_len;

        public Vector3 Position
        {
            get => g_pos;
            set => g_pos = value; 
        }

        public Vector3 Forward
        {
            get => g_forward; 
            set
            {
                Vector3 result = (value - Position);
                g_forward = Vector3.Normalize(result);
                g_forward_len = result.Length();
            }
        }

        public float Speed
        {
            get => g_speed; 
            set => g_speed = value; 
        }

        public Vector3 Scale
        {
            get => g_scale; 
            set => g_scale = value; 
        }
    }
}
