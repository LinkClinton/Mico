using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Mico.Shapes;

namespace Mico.Objects
{
    using NetMath = System.Math;

    public class SphereCollider : Collider
    {
        float radius;

        public SphereCollider()
        {
            Center = Vector3.Zero;
            Radius = 1;
        }

        public SphereCollider(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }
        protected override bool Intersects(BoxCollider collider)
        {
            throw new NotImplementedException();
        }

        protected override bool Intersects(SphereCollider collider)
        {
            throw new NotImplementedException();
        }

        public static SphereCollider Transform(SphereCollider collider, Matrix4x4 matrix)
        {
            SphereCollider result = new SphereCollider();

            Matrix4x4.Decompose(matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation);

            result.Radius = collider.Radius * NetMath.Max(scale.X, NetMath.Max(scale.Y, scale.Z));
            result.Center = collider.Center + translation;

            return result;
        }

        public float Radius
        {
            get => radius;
            set => radius = value;
        }
    }
}
