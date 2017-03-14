using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample
{

    class Program
    {
       

        static void Main(string[] args)
        {
            //System.Numerics.Vector2 simd = new Mico.Math.TVector2(0, 0);
            //System.Numerics.Vector2 temp = new Mico.Math.TVector2(1, 1);

            /*DateTime Now = DateTime.Now;
            for (int i = 1; i <= 10000000; i++)
            {
                System.Numerics.Vector2 vec = new Mico.Math.TVector2(1, 1);
            }
            Console.WriteLine("Used: " + (DateTime.Now - Now).TotalSeconds);
            Console.ReadKey();*/

            Window window = new Window();
            window.Run();
            
        }
    }
}
