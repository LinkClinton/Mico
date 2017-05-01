using System;
using System.Collections;
using System.Collections.Generic;

using Mico.Shapes;
using Mico.Objects;


namespace Mico
{
    public static class Micos
    {

        static List<Shape> m_shapes_list = new List<Shape>();
        static Camera m_camera = new Camera();

        static List<Shape> m_add_list = new List<Shape>();
        static List<Shape> m_remove_list = new List<Shape>();

        static List<IEnumerator> m_enumerator = new List<IEnumerator>();

        public static void Add(Shape shape, object Unknown = null)
        { 
            shape.OnCreate(Unknown);
            m_add_list.Add(shape);
        }

        public static void Remove(Shape shape, object Unknown = null)
        {
            shape.OnDelete(Unknown);
            m_remove_list.Add(shape);
        }

        public static int StartCoutine(IEnumerator countine)
        {
            m_enumerator.Add(countine);
            return m_enumerator.Count;
        }

        public static void StopCoutine(int countineID)
        {
            m_enumerator[countineID].Reset();
            m_enumerator.RemoveAt(countineID);
        }

        public static void Exports(object Unknown = null)
        {
            foreach (Shape item in m_shapes_list)
            {
                item.OnExport(Unknown);
            }

            m_camera.OnExport(Unknown);
        }

        static void FixUpdate()
        {
            int ItemCount = 0;
            foreach (Shape item in m_shapes_list)
            {
                item.FixUpdate();

                ItemCount++;
                //Intersects Test,The algorithm is O(N^2).
                //need update it.
                if (item.Collider != null)
                {
                    for (int i = ItemCount; i < m_shapes_list.Count; i++)
                    {
                        Shape target = m_shapes_list[i];
                        if (target.Collider is null) continue;

                        if (item.Collider.Intersects(target.Collider) is true)
                        {
                            item.OnCollide(target);
                            target.OnCollide(item);
                        }
                    } 
                }

            }
            m_camera.FixUpdate();
        }

        static void UpdateShapeList()
        {
            foreach (Shape item in m_add_list)
                m_shapes_list.Add(item);

            foreach (Shape item in m_remove_list)
                m_shapes_list.Remove(item);

            m_add_list.Clear();
            m_remove_list.Clear();
        }

        public static void Update()
        {
            Time.Update();

            UpdateShapeList();

            foreach (Shape item in m_shapes_list)
            {
                item.OnUpdate();
            }
            m_camera.OnUpdate();


            FixUpdate();

            foreach (var item in m_enumerator)
            {
                item.MoveNext();
            }

         
        }

        public static Camera Camera
        {
            get => m_camera;
            set => m_camera = value;
        }

        public static List<Shape> Element
        {
            get => m_shapes_list;
        }

       

    }
}
