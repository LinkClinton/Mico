using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shapes;

namespace Mico.Objects
{
    public class BoxCollider : Collider
    {
        TVector3 g_radius = new TVector3(0, 0, 0);

        public BoxCollider(Shape shape)
        {
            g_parent = shape;
            g_type = Type.eBox;
        }


        public TVector3 Radius
        {
            get => g_radius;
            set => g_radius = value;
        }
    }
}
