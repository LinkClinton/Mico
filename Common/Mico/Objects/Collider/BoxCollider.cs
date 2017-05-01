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

        private static void GetWireFrame(BoxCollider collider, out Vector3[] box)
        {
            box = new Vector3[8];
            Vector3 temp = collider.m_radius;

            //left-bottom-front
            box[0] = -temp;

            //right-bottom-front
            temp.X = -temp.X;
            box[1] = -temp;

            //right-top-front
            temp.Y = -temp.Y;
            box[2] = -temp;

            //left-top-front
            temp.X = -temp.X;
            box[3] = -temp;

            //left-bottom-back
            temp.Z = -temp.Z;
            temp.Y = -temp.Y;
            box[4] = -temp;

            //right-bottom-back
            temp.X = -temp.X;
            box[5] = -temp;

            //right-top-back
            temp.Y = -temp.Y;
            box[6] = -temp;

            //left-top-back
            temp.X = -temp.X;
            box[7] = -temp;

            for (int i = 0; i < 8; i++)
            {
                box[i] = Vector3.Transform(box[i], collider.m_rotate);
                box[i] += collider.m_center;
            }


        }

        private static void GetMaxMinInAxis(Vector3 axis, Vector3[] box, out float min, out float max)
        {
            Vector3 normal = Vector3.Normalize(box[0]);
            min = Vector3.Dot(normal, axis) * box[0].Length();
            max = Vector3.Dot(normal, axis) * box[0].Length();

            for (int i = 1; i < 7; i++)
            {
                float value = Vector3.Dot(Vector3.Normalize(box[i]), axis) * box[i].Length();
                min = NetMath.Min(min, value);
                max = NetMath.Max(max, value);
            }
        }

        //x-y-z 0-1-2
        private static Vector3 GetAxis(int index, Vector3[] box)
        {
            switch (index)
            {
                case 0:
                    return Vector3.Normalize(box[1] - box[0]);
                case 1:
                    return Vector3.Normalize(box[2] - box[1]);
                case 2:
                    return Vector3.Normalize(box[4] - box[0]);
                default:
                    throw new NotImplementedException("Invaild Index");
            }
        }

        protected override bool Intersects(BoxCollider collider)
        {
            //Get WireFrame
            GetWireFrame(this, out Vector3[] Box);
            GetWireFrame(collider, out Vector3[] ColliderBox);

            //This's axis
            for (int i = 0; i < 3; i++)
            {
                Vector3 Axis = GetAxis(i, Box);
                GetMaxMinInAxis(Axis, Box, out float Min, out float Max);
                GetMaxMinInAxis(Axis, ColliderBox, out float ColliderMin, out float ColliderMax);

                if (ColliderMax < Min || Max < ColliderMin) return false;
            }

            //Collider's axis
            for (int i = 0; i < 3; i++)
            {
                Vector3 Axis = GetAxis(i, ColliderBox);
                GetMaxMinInAxis(Axis, Box, out float Min, out float Max);
                GetMaxMinInAxis(Axis, ColliderBox, out float ColliderMin, out float ColliderMax);

                if (ColliderMax < Min || Max < ColliderMin) return false;
            }

            //Axis Cross Axis
            for (int i = 0; i < 3; i++) //This
            {
                for (int j = 0; j < 3; j++) //Collider
                {
                    Vector3 Axis = Vector3.Cross(GetAxis(i, Box), GetAxis(j, ColliderBox));
                    Axis = Vector3.Normalize(Axis);

                    GetMaxMinInAxis(Axis, Box, out float Min, out float Max);
                    GetMaxMinInAxis(Axis, ColliderBox, out float ColliderMin, out float ColliderMax);

                    if (ColliderMax < Min || Max < ColliderMin) return false;
                }
            }
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
