using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


using Mico.Math;
using Mico.Shapes;

namespace Mico.Objects
{
    public class Camera : Shape
    {
        Vector3 m_up;

        Matrix4x4 m_project;

        public Camera()
        {
            m_up = new Vector3(0, 1, 0);
            m_project = Matrix4x4.Identity;
        }
        
        public Vector3 Up
        {
            get => m_up;
            set => m_up = value;
        }

        public static implicit operator Matrix4x4(Camera camera)
        {
            return TMatrix.CreateLookAtLH(camera.Transform.Position,
                camera.Transform.Position + camera.Transform.Forward, camera.Up);
        }

        public Matrix4x4 Project
        {
            get => m_project;
            set => m_project = value;
        }

        public static float NdcX(float x, float width) => 2.0f * x / width - 1.0f;

        public static float NdcY(float y, float height) => -2.0f * y / height + 1.0f;
    }
}
