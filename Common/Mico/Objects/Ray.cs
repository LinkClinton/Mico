using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;


namespace Mico.Objects
{
    using NetMath = System.Math;

    public class Ray
    {
        Vector3 m_position;
        Vector3 m_direction;

        public Ray()
        {
            m_position = Vector3.Zero;
            m_direction = Vector3.UnitZ;
        }

        public Ray(Vector3 position, Vector3 direction)
        {
            m_position = position;
            m_direction = direction;
        }

        public bool Intersects(Collider collider)
        {
            return Intersects(collider, out float distance);
        }

        public bool Intersects(Collider collider, out float distance)
        {
            switch (collider)
            {
                case BoxCollider box:
                    return Intersects(box, out distance);
                case SphereCollider sphere:
                    return Intersects(sphere, out distance);
                default:
                    throw new NotImplementedException("Unknown Type");
            }
        }

        private bool Intersects(BoxCollider box, out float distance)
        {
            //from Real-Time Rendering
            //The ray is in world spcae

            distance = 0;

            Vector3 d = Vector3.Normalize(m_direction);
            Vector3 p = box.Center - m_position;

            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(box.Rotate);

            Vector3[] axis = new Vector3[3];
            
            axis[0] = new Vector3(m.M11, m.M12, m.M13);
            axis[1] = new Vector3(m.M21, m.M22, m.M23);
            axis[2] = new Vector3(m.M31, m.M32, m.M33);

            float[] h = new float[3] { box.Radius.X, box.Radius.Y, box.Radius.Z };
            
            float tmin = -float.MaxValue;
            float tmax = float.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                Vector3 item = axis[i];
                float radius = h[i];

                float e = Vector3.Dot(item, p);
                float f = Vector3.Dot(item, d);

                float t1 = (e + radius) / f;
                float t2 = (e - radius) / f;

                if (t1 > t2)
                {
                    float temp = t1; t1 = t2; t2 = temp;
                }

                tmin = NetMath.Max(tmin, t1);
                tmax = NetMath.Min(tmax, t2);

                if (tmin > tmax) return false;
                if (tmax < 0) return false;
            }

            if (tmin > 0) distance = tmin;
            else distance = tmax;

            return true;
        }

        private bool Intersects(SphereCollider sphere, out float distance)
        {
            //from Real-Time Rendering
            //The ray is in world spcae
            distance = 0;

            Vector3 l = sphere.Center - m_position;
            Vector3 d = Vector3.Normalize(m_direction);

            float s = Vector3.Dot(l, d);
            float l_2 = Vector3.Dot(l, l);
            float r_2 = sphere.Radius * sphere.Radius;

            if (s < 0 && l_2 > r_2) return false;

            float m_2 = l_2 - s*s;

            if (m_2 > r_2) return false;

            float q = (float)NetMath.Sqrt(r_2 - m_2);
            if (l_2 > r_2) distance = s - q;
            else distance = s + q;

            return true;
        }

        public override string ToString()
        {
            return m_position.X + " " + m_position.Y + " " + m_position.Z + Environment.NewLine
                + m_direction.X + " " + m_direction.Y + " " + m_direction.Z + Environment.NewLine;
        }

        public Vector3 Position
        {
            get => m_position;
            set => m_position = value;
        }

        public Vector3 Direction
        {
            get => m_direction;
            set => m_direction = value;
        }

    }
}
