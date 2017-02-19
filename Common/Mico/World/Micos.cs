using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Shapes;

namespace Mico.World
{
    public static class Micos
    {
        static List<Shape> g_shapes = new List<Shape>();

        public static void Add(Shape shape, object Unknown = null)
        {
            shape.OnCreate(Unknown);
            g_shapes.Add(shape);
        }

        public static void Delete(Shape shape, object Unknown = null)
        {
            shape.OnDelete(Unknown);
            g_shapes.Remove(shape);
        }

        public static void Update()
        {
            Time.Update();

            foreach (Shape item in g_shapes)
            {
                item.OnUpdate();
                item.FixUpdate();
            }
        }


    }
}
