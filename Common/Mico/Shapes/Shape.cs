using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Objects;

namespace Mico.Shapes
{
    public class Shape
    {
        Shape g_parent = null;

        Transform g_transform = new Transform();
        Collider g_collider = null;

        public virtual void OnUpdate(object Unknown = null) { }
        public virtual void OnExport(object Unknown = null) { }
        public virtual void OnCreate(object Unknown = null) { }
        public virtual void OnDelete(object Unknown = null) { }
        public virtual void FixUpdate(object Unknown = null) { }


        public Collider Collider
        {
            get => g_collider;
            set => g_collider = value;
        }

        public Transform Transform
        {
            get => g_transform;
            set => g_transform = value;
        }

        public Shape Parent
        {
            get => g_parent;
            set => g_parent = value;
        }
        
    }
}
