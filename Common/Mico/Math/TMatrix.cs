using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Math
{
    public static class TMatrix
    {
        public static Matrix4x4 CreateLookAtLH(Vector3 position, Vector3 lookat, Vector3 up)
        {
            if (position == lookat)
                throw new NotImplementedException("position is same as lookat");

            Vector3 f = Vector3.Normalize(lookat - position);
            Vector3 s = Vector3.Normalize(Vector3.Cross(up, f));
            Vector3 u = Vector3.Cross(f, s);

            return new Matrix4x4()
            {
                M11 = s.X,
                M21 = s.Y,
                M31 = s.Z,
                M12 = u.X,
                M22 = u.Y,
                M32 = u.Z,
                M13 = f.X,
                M23 = f.Y,
                M33 = f.Z,
                M41 = -Vector3.Dot(s, position),
                M42 = -Vector3.Dot(u, position),
                M43 = -Vector3.Dot(f, position),
            };
        }

        public static Matrix4x4 CreatePerspectiveFieldOfViewLH(
            float fieldOfView, float aspectRatio,
            float nearPlaneDistance, float farPlaneDistance)
        {
            float tanHallView = (float)System.Math.Tan(fieldOfView / 2.0);

            Matrix4x4 result = new Matrix4x4()
            {
                M11 = 1 / (aspectRatio * tanHallView),
                M22 = 1 / tanHallView,
                M34 = 1,
                M33 = nearPlaneDistance / (farPlaneDistance - nearPlaneDistance),
                M43 = -(farPlaneDistance * nearPlaneDistance) / (farPlaneDistance - nearPlaneDistance)
            };
            return result;
        }



    }


}
