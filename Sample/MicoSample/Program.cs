using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicoSample
{
    class MySurface : Mico.Surface.Surface
    {
        public MySurface()
        {
            CreateWindow("Mico", "", 800, 600);
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MySurface surface = new MySurface();
            surface.Run();

        }
    }
}
