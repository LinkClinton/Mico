using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Mico.Objects
{
    public abstract class Collider
    {
        protected Vector3 m_center = Vector3.Zero;

        protected abstract bool Intersects(BoxCollider collider);

        protected abstract bool Intersects(SphereCollider collider);

        public abstract void Transform(Matrix4x4 matrix);

        public abstract void Transform(Vector3 translation, Quaternion rotation, Vector3 scale);

        bool m_ispicked = true;

        public bool Intersects(Collider collider)
        {
            switch (collider)
            {
                case BoxCollider box:
                    return Intersects(box);
                case SphereCollider sphere:
                    return Intersects(sphere);
                default:
                    return false;
            }
        }

        public bool Intersects(Ray ray)
        {
            return ray.Intersects(this);
        }

        public bool Intersects(Ray ray,out float distance)
        {
            return ray.Intersects(this, out distance);
        }

        public Vector3 Center
        {
            get => m_center;
            set => m_center = value;
        }

        public bool IsPicked
        {
            get => m_ispicked;
            set => m_ispicked = value;
        }
    }
}
