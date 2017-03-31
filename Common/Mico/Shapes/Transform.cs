using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico.Math;



namespace Mico.Shapes
{
    using NetMath = System.Math;

    public class Transform
    {
        Vector3 g_pos = new Vector3();
        Vector3 g_forward = TVector3.Forward;
        Vector3 g_scale = new Vector3(1, 1, 1);
        Vector3 g_rotate = new Vector3(0, 0, 0);//eular heading-pitch-bank

        float g_forward_len;

        public Vector3 Position
        {
            get => g_pos;
            set => g_pos = value; 
        }

        public Vector3 Forward
        {
            get => g_forward; 
            set
            {
                float arc = (float)(180 / NetMath.PI);

                g_rotate.X = (float)NetMath.Atan2(value.Y, new Vector2(value.X, value.Z).Length()) * arc;
                g_rotate.Y = (float)NetMath.Atan2(value.X, value.Z) * arc;

                g_forward = Vector3.Normalize(value);
                g_forward_len = value.Length();
            }
        }

        public Vector3 Rotate
        {
            get => g_rotate;
            set
            {
                float arc = (float)NetMath.PI / 180;

                Vector3 forward = Vector3.UnitZ;
                Matrix4x4 matrix = Matrix4x4.CreateFromYawPitchRoll(value.Y * arc, value.X * arc, value.Z * arc);

                g_forward.X = forward.X * matrix.M11 + forward.Y * matrix.M21 + forward.Z * matrix.M31 + 1 * matrix.M41;
                g_forward.Y = forward.X * matrix.M12 + forward.Y * matrix.M22 + forward.Z * matrix.M32 + 1 * matrix.M42;
                g_forward.Z = forward.X * matrix.M13 + forward.Y * matrix.M23 + forward.Z * matrix.M33 + 1 * matrix.M43;

               
                g_forward_len = g_forward.Length();
                g_forward = Vector3.Normalize(g_forward);


                g_rotate = value;
            }
        }

        public Vector3 Scale
        {
            get => g_scale; 
            set => g_scale = value; 
        }
    }
}
