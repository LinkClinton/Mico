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
        Vector3 g_lookat = new Vector3(0, 0, 0);

        public Camera()
        {

        }
        
        public Vector3 LookAt
        {
            get => g_lookat;
            set => g_lookat = value; 
        }


    }
}
