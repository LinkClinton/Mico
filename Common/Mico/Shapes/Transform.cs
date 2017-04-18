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
        Quaternion g_rotate = Quaternion.Identity;

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

                //update Quaternion
            }
        }

        public Quaternion Rotate
        {
            get => g_rotate;
            set
            {
                g_rotate = value;

                g_forward = Vector3.Transform(g_forward, g_rotate);
                g_forward_len = g_forward.Length();
                g_forward = Vector3.Normalize(g_forward);
            }
        }

        public Vector3 Scale
        {
            get => g_scale; 
            set => g_scale = value; 
        }

        public static implicit operator Matrix4x4(Transform transform)
        {
            Matrix4x4 result = Matrix4x4.Identity;

            result *= Matrix4x4.CreateScale(transform.Scale);

            result *= Matrix4x4.CreateFromQuaternion(transform.g_rotate);

            result *= Matrix4x4.CreateTranslation(transform.Position);

            return result;
        }
    }
}
