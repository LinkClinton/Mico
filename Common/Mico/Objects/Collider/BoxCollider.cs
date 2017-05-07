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
        Vector3 m_radius;
        Quaternion m_rotate;

        protected override bool Intersects(BoxCollider collider)
        {
            // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
            // ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
            // THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
            // PARTICULAR PURPOSE.
            //  
            // Copyright (c) Microsoft Corporation. All rights reserved.

            Quaternion quaternion = m_rotate * Quaternion.Conjugate(collider.m_rotate);
            Matrix4x4 R = Matrix4x4.CreateFromQuaternion(quaternion);
            Matrix4x4 AR = Math.TMatrix.Abs(R);

            Vector3 t = Vector3.Transform(collider.m_center - m_center,
                Quaternion.Inverse(m_rotate));

            float d, d_A, d_B;

            // l = a(u) = (1, 0, 0)
            // t dot l = t.x
            // d(A) = h(A).x
            // d(B) = h(B) dot abs(r00, r01, r02)
            d = t.X;
            d_A = m_radius.X;
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M11, AR.M12, AR.M13));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(v) = (0, 1, 0)
            // t dot l = t.y
            // d(A) = h(A).y
            // d(B) = h(B) dot abs(r10, r11, r12)
            d = t.Y;
            d_A = m_radius.Y;
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M21, AR.M22, AR.M23));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(w) = (0, 0, 1)
            // t dot l = t.z
            // d(A) = h(A).z
            // d(B) = h(B) dot abs(r20, r21, r22)
            d = t.Z;
            d_A = m_radius.Z;
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M31, AR.M32, AR.M33));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = b(u) = (r00, r10, r20)
            // d(A) = h(A) dot abs(r00, r10, r20)
            // d(B) = h(B).x
            d = Vector3.Dot(t, new Vector3(R.M11, R.M21, R.M31));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M11, AR.M21, AR.M31));
            d_B = collider.m_radius.X;
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = b(v) = (r01, r11, r21)
            // d(A) = h(A) dot abs(r01, r11, r21)
            // d(B) = h(B).y
            d = Vector3.Dot(t, new Vector3(R.M12, R.M22, R.M32));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M12, AR.M22, AR.M32));
            d_B = collider.m_radius.Y;
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = b(w) = (r02, r12, r22)
            // d(A) = h(A) dot abs(r02, r12, r22)
            // d(B) = h(B).z
            d = Vector3.Dot(t, new Vector3(R.M13, R.M23, R.M33));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M13, AR.M23, AR.M33));
            d_B = collider.m_radius.Z;
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(u) x b(u) = (0, -r20, r10)
            // d(A) = h(A) dot abs(0, r20, r10)
            // d(B) = h(B) dot abs(0, r02, r01)
            d = Vector3.Dot(t, new Vector3(0, -R.M31, R.M21));
            d_A = Vector3.Dot(m_radius, new Vector3(0, AR.M31, AR.M21));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(0, AR.M13, AR.M12));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(u) x b(v) = (0, -r21, r11)
            // d(A) = h(A) dot abs(0, r21, r11)
            // d(B) = h(B) dot abs(r02, 0, r00)
            d = Vector3.Dot(t, new Vector3(0, -R.M32, R.M22));
            d_A = Vector3.Dot(m_radius, new Vector3(0, AR.M32, AR.M22));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M13, 0, AR.M11));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(u) x b(w) = (0, -r22, r12)
            // d(A) = h(A) dot abs(0, r22, r12)
            // d(B) = h(B) dot abs(r01, r00, 0)
            d = Vector3.Dot(t, new Vector3(0, -R.M33, -R.M23));
            d_A = Vector3.Dot(m_radius, new Vector3(0, AR.M33, AR.M23));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M12, AR.M11, 0));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(v) x b(u) = (r20, 0, -r00)
            // d(A) = h(A) dot abs(r20, 0, r00)
            // d(B) = h(B) dot abs(0, r12, r11)
            d = Vector3.Dot(t, new Vector3(R.M31, 0, -R.M11));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M31, 0, AR.M11));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(0, AR.M23, AR.M22));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(v) x b(v) = (r21, 0, -r01)
            // d(A) = h(A) dot abs(r21, 0, r01)
            // d(B) = h(B) dot abs(r12, 0, r10)
            d = Vector3.Dot(t, new Vector3(R.M32, 0, -R.M12));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M32, 0, AR.M12));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M23, 0, AR.M21));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(v) x b(w) = (r22, 0, -r02)
            // d(A) = h(A) dot abs(r22, 0, r02)
            // d(B) = h(B) dot abs(r11, r10, 0)
            d = Vector3.Dot(t, new Vector3(R.M33, 0, -R.M13));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M33, 0, AR.M13));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M22, AR.M21, 0));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(w) x b(u) = (-r10, r00, 0)
            // d(A) = h(A) dot abs(r10, r00, 0)
            // d(B) = h(B) dot abs(0, r22, r21)
            d = Vector3.Dot(t, new Vector3(-R.M21, R.M11, 0));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M21, AR.M11, 0));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(0, AR.M33, AR.M32));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(w) x b(v) = (-r11, r01, 0)
            // d(A) = h(A) dot abs(r11, r01, 0)
            // d(B) = h(B) dot abs(r22, 0, r20)
            d = Vector3.Dot(t, new Vector3(-R.M22, R.M12, 0));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M22, AR.M12, 0));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M33, 0, AR.M31));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;

            // l = a(w) x b(w) = (-r12, r02, 0)
            // d(A) = h(A) dot abs(r12, r02, 0)
            // d(B) = h(B) dot abs(r21, r20, 0)
            d = Vector3.Dot(t, new Vector3(-R.M23, R.M13, 0));
            d_A = Vector3.Dot(m_radius, new Vector3(AR.M23, AR.M13, 0));
            d_B = Vector3.Dot(collider.m_radius, new Vector3(AR.M32, AR.M31, 0));
            if (NetMath.Abs(d) > NetMath.Abs(d_A) + NetMath.Abs(d_B)) return false;
        
            return true;
        }

        protected override bool Intersects(SphereCollider collider)
        {
            //the sphere's center is a vector.
            //transform it from world coord-system to box's local coord-system

            Vector3 SphereCenter = Vector3.Transform(collider.Center - m_center,
                Quaternion.Inverse(m_rotate));

            //you can think it is a AABB Box.

            //x-axis
            if (NetMath.Abs(SphereCenter.X) > collider.Radius + Radius.X) return false;
            //y-axis
            if (NetMath.Abs(SphereCenter.Y) > collider.Radius + Radius.Y) return false;
            //z-axis
            if (NetMath.Abs(SphereCenter.Z) > collider.Radius + Radius.Z) return false;

            return true;
        }

        public override void Transform(Matrix4x4 matrix)
        {
            Matrix4x4.Decompose(matrix, out Vector3 scale,
                out Quaternion rotation, out Vector3 translation);

            m_rotate *= rotation;
            m_radius *= scale;
            m_center += translation;
        }

        public override void Transform(Vector3 translation, Quaternion rotation, Vector3 scale)
        {
            m_rotate *= rotation;
            m_radius *= scale;
            m_center += translation;
        }

        public override string ToString()
        {
            return m_center.X + " " + m_center.Y + " " + m_center.Z + Environment.NewLine
                + m_radius.X + " " + m_radius.Y + " " + m_radius.Z + Environment.NewLine
                + m_rotate.X + " " + m_rotate.Y + " " + m_rotate.Z + " " + m_rotate.W + Environment.NewLine;
        }

        public BoxCollider()
        {
            m_radius = Vector3.One;
            m_rotate = Quaternion.Identity;
        }

        public BoxCollider(Vector3 center, Vector3 radius)
        {
            Center = center;
            Radius = radius;
        }

        public static BoxCollider Transform(BoxCollider collider, Matrix4x4 matrix)
        {
            BoxCollider result = new BoxCollider();

            Matrix4x4.Decompose(matrix, out Vector3 scale, 
                out Quaternion rotation, out Vector3 translation);

            result.Rotate = collider.m_rotate * rotation;
            result.Radius = collider.m_radius * scale;
            result.Center = Vector3.Transform(collider.m_center, matrix);
            
            return result;
        }

        public Vector3 Radius
        {
            get => m_radius;
            set => m_radius = value;
        }

        public Quaternion Rotate
        {
            get => m_rotate;
            set => m_rotate = value;
        }
    }
}
