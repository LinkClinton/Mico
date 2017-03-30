using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Mico.Shapes;
namespace Mico.Diep.Sample
{

    class Program
    {

    

        static void Main(string[] args)
        {
            Shape a = new Shape();
            float PI = (float)System.Math.PI;
            a.Transform.Rotate = new System.Numerics.Vector3(PI, 0, 0);
            Console.WriteLine(a.Transform.Forward);

            Window window = new Window();
            window.Run();
        }
    }
}
