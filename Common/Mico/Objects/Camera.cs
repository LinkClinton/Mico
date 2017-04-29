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

        public Camera()
        {
            m_up = new Vector3(0, 1, 0);
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
    }
}
