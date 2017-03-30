using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico.Math;



namespace Mico.Shapes
{
    using NetMath = System.Math;

    public class Transform
    {
        Vector3 g_pos = new Vector3();
        Vector3 g_forward = TVector3.Forward;
        Vector3 g_scale = new Vector3(1, 1, 1);
        Vector3 g_rotate = new Vector3(0, 0, 0);

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
              

                g_forward = Vector3.Normalize(value);
                g_forward_len = value.Length();
            }
        }

        public Vector3 Rotate
        {
            get => g_rotate;
            set
            {
                
             
                g_rotate = value;
            }
        }

        public Vector3 Scale
        {
            get => g_scale; 
            set => g_scale = value; 
        }
    }
}
