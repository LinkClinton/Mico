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
            a.Collider = new BoxCollider(a);
            (a.Collider as BoxCollider).Radius = new System.Numerics.Vector3(1, 1, 1);
            a.Transform.Rotate = new System.Numerics.Vector3(90, 45, 0);

            World.Micos.Add(a);
            World.Micos.Update();

            Console.ReadKey();
            //Window window = new Window();
            //window.Run();
        }
    }
}
