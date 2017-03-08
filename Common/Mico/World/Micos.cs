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
        static Camera g_camera = new Camera();

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

        public static void Exports(object Unknown = null)
        {
            foreach (Shape item in g_shapes)
            {
                item.OnRender(Unknown);
            }

            g_camera.OnRender(Unknown);
        }

        public static void Update()
        {
            Time.Update();

            foreach (Shape item in g_shapes)
            {
                item.OnUpdate();
                item.FixUpdate();
            }

            g_camera.OnUpdate();
            g_camera.FixUpdate();
        }

        public static Camera Camera
        {
            get => g_camera;
            set => g_camera = value;
        }


    }
}
