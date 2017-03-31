using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Mico.Shapes
{
    using NetMath = System.Math;

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

            public void Update(Vector3 vec)
            {
                XAxis.X = NetMath.Min(XAxis.X, vec.X);
                XAxis.Y = NetMath.Max(XAxis.Y, vec.X);

                YAxis.X = NetMath.Min(YAxis.X, vec.Y);
                YAxis.Y = NetMath.Max(YAxis.Y, vec.Y);

                ZAxis.X = NetMath.Min(ZAxis.X, vec.Z);
                ZAxis.Y = NetMath.Max(ZAxis.Y, vec.Z);
            }
        }

    }

}
