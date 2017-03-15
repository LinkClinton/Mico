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

  

        public Vector3 Scale
        {
            get => g_scale; 
            set => g_scale = value; 
        }




        public static implicit operator Matrix3x2(Transform transform)
        {
            Matrix3x2 matrix = Matrix3x2.Identity;

            float angle = (float)System.Math.Atan2(transform.Forward.Y, transform.Forward.X);
            
            matrix *= Matrix3x2.CreateRotation(angle);
            matrix *= Matrix3x2.CreateScale(transform.Scale.X,transform.Scale.Y);
            matrix *= Matrix3x2.CreateTranslation(transform.Position.X, transform.Position.Y);
         
            return matrix;
        }
    }
}
