using System;

using Mico.Objects;
using Mico.Shapes;
using System.Collections.Generic;

namespace Mico.World
{
    public static class Micos
    {

        static List<Shape> g_shapes_list = new List<Shape>();
        static Camera g_camera = new Camera();

        static List<Shape> g_add_list = new List<Shape>();
        static List<Shape> g_remove_list = new List<Shape>();

        public static void Add(Shape shape, object Unknown = null)
        {
            shape.OnCreate(Unknown);
            g_add_list.Add(shape);
        }

        public static void Remove(Shape shape, object Unknown = null)
        {
            shape.OnDelete(Unknown);
            g_remove_list.Add(shape);
        }

        public static void Exports(object Unknown = null)
        {
            foreach (Shape item in g_shapes_list)
            {
                item.OnExport(Unknown);
            }

            g_camera.OnExport(Unknown);
        }

        static void FixUpdate()
        {
            foreach (Shape item in g_shapes_list)
            {
                item.FixUpdate();
            }
            g_camera.FixUpdate();
        }

        static void UpdateShapeList()
        {
            foreach (Shape item in g_add_list)
                g_shapes_list.Add(item);

            foreach (Shape item in g_remove_list)
                g_shapes_list.Remove(item);

            g_add_list.Clear();
            g_remove_list.Clear();
        }

        public static void Update()
        {
            Time.Update();

            foreach (Shape item in g_shapes_list)
            {
                item.OnUpdate();
            }
            g_camera.OnUpdate();


            FixUpdate();

            UpdateShapeList();
        }

        public static Camera Camera
        {
            get => g_camera;
            set => g_camera = value;
        }


       

    }
}
