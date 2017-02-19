using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicoSample
{
    class People : Mico.Shapes.Shape
    {
        string g_Name;

        public People(string Name)
        {
            g_Name = Name;
            Mico.World.Micos.Add(this);
        }

        public override void OnRender(object Unknown = null)
        {
            Console.WriteLine(g_Name + "'s Position=" + Transform.Position);
            base.OnRender(Unknown);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
