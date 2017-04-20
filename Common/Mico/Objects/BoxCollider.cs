using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Objects
{
    using NetMath = System.Math;

    public class BoxCollider : Collider
    {
        Vector3 radius = Vector3.One;
        Quaternion rotate = Quaternion.Identity;

        public BoxCollider()
        { 
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
            //the sphere's center is a vector.
            //transform it from world coord-system to box's local coord-system

            Vector3 SphereCenter = Vector3.Transform(collider.Center - Center,
                Quaternion.Inverse(Rotate));

            //you can think it is a AABB Box.

            //x-axis
            if (NetMath.Abs(SphereCenter.X) > collider.Radius + Radius.X) return false;
            //y-axis
            if (NetMath.Abs(SphereCenter.Y) > collider.Radius + Radius.Y) return false;
            //z-axis
            if (NetMath.Abs(SphereCenter.Z) > collider.Radius + Radius.Z) return false;

            return true;
        }

        public static BoxCollider Transform(BoxCollider collider, Matrix4x4 matrix)
        {
            BoxCollider result = new BoxCollider();

            Matrix4x4.Decompose(matrix, out Vector3 scale, 
                out Quaternion rotation, out Vector3 translation);

            result.Rotate = collider.Rotate * rotation;
            result.Radius = collider.Radius * scale;
            result.Center = Vector3.Transform(collider.Center, matrix);
            
            return result;
        }

        public Vector3 Radius
        {
            get => radius;
            set => radius = value;
        }

        public Quaternion Rotate
        {
            get => rotate;
            set => rotate = value;
        }
    }
}
