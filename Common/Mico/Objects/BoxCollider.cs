using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Objects
{
    public class BoxCollider : Collider
    {
        Vector3 radius;
        Vector3 rotate;

        public BoxCollider()
        {
            Center = Vector3.Zero;
            Radius = Vector3.One;
        }

        public BoxCollider(Vector3 center, Vector3 radius)
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

        public static BoxCollider Transform(BoxCollider collider, Matrix4x4 matrix)
        {
            BoxCollider result = new BoxCollider();

            Matrix4x4.Decompose(matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation);


            result.Radius = collider.Radius * scale;
            result.Center = collider.Center + translation;
            //

            return result;
        }

        public Vector3 Radius
        {
            get => radius;
            set => radius = value;
        }

        public Vector3 Rotate
        {
            get => rotate;
            set => rotate = value;
        }
    }
}
