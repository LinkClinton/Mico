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
                //update Quaternion

                //x and z default is (0,1)
                float angle_y = (float)NetMath.Atan2(-value.X, value.Z);
                float angle_x = (float)NetMath.Atan2(value.Y, value.Z);

                g_rotate = Quaternion.CreateFromYawPitchRoll(angle_y, angle_x, 0);
            }
        }

        public Quaternion Rotate
        {
            get => g_rotate;
            set
            {
                g_rotate = value;

                g_forward = Vector3.Transform(g_forward, g_rotate);
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
