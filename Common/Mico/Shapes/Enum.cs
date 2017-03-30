using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Mico.Shapes
{
    public partial class Collider
    {
        public enum Type
        {
            eBox,
            eSphere,
            UnKnown
        }

        protected struct Projection
        {
            public Vector2 XAxis;
            public Vector2 YAxis;
            public Vector2 ZAxis;
        }

    }

}
