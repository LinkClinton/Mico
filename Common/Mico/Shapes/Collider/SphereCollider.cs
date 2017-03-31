using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shapes;

namespace Mico.Shapes
{
    public class SphereCollider : Collider
    {
        float g_radius = 0;


        protected override void UpdateProjection()
        {
            Vector3 global = GlobalCenter;

            g_project = new Projection()
            {
                XAxis = new Vector2(global.X - Radius, global.X + Radius),
                YAxis = new Vector2(global.Y - Radius, global.Y + Radius),
                ZAxis = new Vector2(global.Z - Radius, global.Z + Radius)
            };

            base.UpdateProjection();
        }

        public SphereCollider(Shape shape)
        {
            g_parent = shape;
            g_type = Type.eSphere;
        }

        public float Radius
        {
            get => g_radius;
            set => g_radius = value;
        }

    }
}
