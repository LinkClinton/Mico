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
        Vector3 m_pos = new Vector3();
        Vector3 m_forward = TVector3.Forward;
        Vector3 m_scale = new Vector3(1, 1, 1);
        Quaternion m_rotate = Quaternion.Identity;


        public Vector3 Position
        {
            get => m_pos;
            set => m_pos = value; 
        }

        public Vector3 Forward
        {
            get => m_forward; 
            set
            {
                m_forward = Vector3.Normalize(value);
                //update Quaternion

                //x and z default is (0,1)
                float angle_y = (float)NetMath.Atan2(-value.X, value.Z);
                float angle_x = (float)NetMath.Atan2(value.Y, value.Z);

                m_rotate = Quaternion.CreateFromYawPitchRoll(angle_y, angle_x, 0);
            }
        }

        public Quaternion Rotate
        {
            get => m_rotate;
            set
            {
                m_rotate = value;

                m_forward = Vector3.Transform(m_forward, m_rotate);
                m_forward = Vector3.Normalize(m_forward);
            }
        }

        public Vector3 Scale
        {
            get => m_scale; 
            set => m_scale = value; 
        }

        public static implicit operator Matrix4x4(Transform transform)
        {
            Matrix4x4 result = Matrix4x4.Identity;

            result *= Matrix4x4.CreateScale(transform.Scale);

            result *= Matrix4x4.CreateFromQuaternion(transform.m_rotate);

            result *= Matrix4x4.CreateTranslation(transform.Position);

            return result;
        }
    }
}
