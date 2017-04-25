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
        Vector3 radius;
        Quaternion rotate;

        private static void GetWireFrame(BoxCollider collider, out Vector3[] box)
        {
            box = new Vector3[8];
            Vector3 temp = collider.radius;

            //left-bottom-front
            box[0] = collider.center - temp;

            //right-bottom-front
            temp.X = -temp.X;
            box[1] = collider.center - temp;

            //right-top-front
            temp.Y = -temp.Y;
            box[2] = collider.center - temp;

            //left-top-front
            temp.X = -temp.X;
            box[3] = collider.center - temp;

            //left-bottom-back
            temp.Z = -temp.Z;
            temp.Y = -temp.Y;
            box[4] = collider.center - temp;

            //right-bottom-back
            temp.X = -temp.X;
            box[5] = collider.center - temp;

            //right-top-back
            temp.Y = -temp.Y;
            box[6] = collider.center - temp;

            //left-top-back
            temp.X = -temp.X;
            box[7] = collider.center - temp;

            for (int i = 0; i < 8; i++)
                box[i] = Vector3.Transform(box[i], collider.rotate);
        }

        private static void GetMaxMinInAxis(Vector3 axis, Vector3[] box, out float min, out float max)
        {
            min = Vector3.Dot(box[0], axis) * box[0].Length();
            max = Vector3.Dot(box[0], axis) * box[0].Length();

            for (int i = 1; i < 7; i++)
            {
                float value = Vector3.Dot(box[i], axis) * box[i].Length();
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

            Vector3 SphereCenter = Vector3.Transform(collider.Center - Center,
                Quaternion.Inverse(Rotate));

            //you can think it is a AABB Box.

            //x-axis
            if (NetMath.Abs(SphereCenter.X) > collider.Radius + Radius.X) return false;
            //y-axis
            if (NetMath.Abs(SphereCenter.Y) > collider.Radius + Radius.Y) return false;
            //z-axis
            if (NetMath.Abs(SphereCenter.Z) > collider.Radius + Radius.Z) return false;

            return true;
        }

        public BoxCollider()
        {
            radius = Vector3.One;
            rotate = Quaternion.Identity;
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

            result.Rotate = collider.Rotate * rotation;
            result.Radius = collider.Radius * scale;
            result.Center = Vector3.Transform(collider.Center, matrix);
            
            return result;
        }

        public Vector3 Radius
        {
            get => radius;
            set => radius = value;
        }

        public Quaternion Rotate
        {
            get => rotate;
            set => rotate = value;
        }
    }
}
