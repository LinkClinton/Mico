using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace Mico.Objects
{
    using NetMath = System.Math;

    public class SphereCollider : Collider
    {
        float m_radius = 1;

        public SphereCollider()
        {
        }

        public SphereCollider(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }
        protected override bool Intersects(BoxCollider collider)
        {
            return collider.Intersects(this);
        }

        protected override bool Intersects(SphereCollider collider)
        {
            float radius_limit = m_radius + collider.m_radius;
            if (Vector3.DistanceSquared(m_center, collider.m_center) <
                radius_limit * radius_limit)
                return true;
            return false;
        }

        public static SphereCollider Transform(SphereCollider collider, Matrix4x4 matrix)
        {
            SphereCollider result = new SphereCollider();

            Matrix4x4.Decompose(matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation);

            result.Radius = collider.m_radius * NetMath.Max(scale.X, NetMath.Max(scale.Y, scale.Z));
            result.Center = collider.m_center + translation;

            return result;
        }

        public float Radius
        {
            get => m_radius;
            set => m_radius = value;
        }
    }
}
