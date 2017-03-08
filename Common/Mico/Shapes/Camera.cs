using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Mico.Math;

namespace Mico.Shapes
{
    public class Camera : Shape
    {
        Vector3 g_lookat = new Vector3();

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
