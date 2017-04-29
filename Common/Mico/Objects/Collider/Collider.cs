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

        public Vector3 Center
        {
            get => m_center;
            set => m_center = value;
        }
    }
}
