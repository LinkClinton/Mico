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

        Vector3[] g_origin_box = new Vector3[8];


        public BoxCollider(Shape shape)
        {
            g_parent = shape;
            g_type = Type.eBox;
        }

        protected override void UpdateProjection()
        {
            Vector3[] g_current_box = new Vector3[8];

            float arc = (float)System.Math.PI / 180;

            Matrix4x4 matrix = Matrix4x4.CreateFromYawPitchRoll(Transform.Rotate.Y * arc,
                Transform.Rotate.X * arc, Transform.Rotate.Z * arc) * Matrix4x4.CreateTranslation(Transform.Position);

            for (int i = 0; i < g_current_box.Length; i++)
            {
                Vector3 origin = g_origin_box[i];
                g_current_box[i] = new Vector3()
                {
                    X = origin.X * matrix.M11 + origin.Y * matrix.M21 + origin.Z * matrix.M31 + 1 * matrix.M41,
                    Y = origin.X * matrix.M12 + origin.Y * matrix.M22 + origin.Z * matrix.M32 + 1 * matrix.M42,
                    Z = origin.X * matrix.M13 + origin.Y * matrix.M23 + origin.Z * matrix.M33 + 1 * matrix.M43
                };
            }

            g_project = new Projection()
            {
                XAxis = new Vector2(g_current_box[0].X, g_current_box[0].X),
                YAxis = new Vector2(g_current_box[0].Y, g_current_box[0].Y),
                ZAxis = new Vector2(g_current_box[0].Z, g_current_box[0].Z)
            };

            foreach (var item in g_current_box)
            {
                g_project.Update(item);
            }

            
            base.UpdateProjection();
        }
        


        public Vector3 Radius
        {
            get => g_radius;
            set
            {
                g_origin_box = new Vector3[8]
                {
                    new Vector3(-value.X,-value.Y,-value.Z),
                    new Vector3(-value.X, value.Y,-value.Z),
                    new Vector3(-value.X, value.Y, value.Z),
                    new Vector3(-value.X,-value.Y, value.Z),
                    new Vector3( value.X, value.Y, value.Z),
                    new Vector3( value.X, value.Y,-value.Z),
                    new Vector3( value.X,-value.Y,-value.Z),
                    new Vector3( value.X,-value.Y, value.Z)
                };


                g_radius = value;
            }
        }
    }
}
