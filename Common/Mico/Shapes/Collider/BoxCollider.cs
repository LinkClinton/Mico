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
    public class BoxCollider : Collider
    {
        Vector3 g_radius = new Vector3(0, 0, 0);

        public BoxCollider(Shape shape)
        {
            g_parent = shape;
            g_type = Type.eBox;
        }

        protected override void UpdateProjection()
        {        
            //need update
            base.UpdateProjection();
        }
        


        public Vector3 Radius
        {
            get => g_radius;
            set => g_radius = value;
        }
    }
}
