using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Math
{
    using NetMath = System.Math;

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

        public static Matrix4x4 Abs(Matrix4x4 matrix)
        {
            return new Matrix4x4()
            {
                M11 = NetMath.Abs(matrix.M11), M12 = NetMath.Abs(matrix.M12),
                M13 = NetMath.Abs(matrix.M13), M14 = NetMath.Abs(matrix.M14),
                M21 = NetMath.Abs(matrix.M21), M22 = NetMath.Abs(matrix.M22),
                M23 = NetMath.Abs(matrix.M23), M24 = NetMath.Abs(matrix.M24),
                M31 = NetMath.Abs(matrix.M31), M32 = NetMath.Abs(matrix.M32),
                M33 = NetMath.Abs(matrix.M33), M34 = NetMath.Abs(matrix.M34),
                M41 = NetMath.Abs(matrix.M41), M42 = NetMath.Abs(matrix.M42),
                M43 = NetMath.Abs(matrix.M43), M44 = NetMath.Abs(matrix.M44)
            };
        }

    }


}
