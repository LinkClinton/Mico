using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Objects;

namespace Mico.Shapes
{
    

    public abstract class Shape
    { 
        Shape m_parent = null;

        Transform m_transform = new Transform();

        Collider m_collider;

        protected internal virtual void OnUpdate(object Unknown = null) { }
        protected internal virtual void OnExport(object Unknown = null) { }
        protected internal virtual void OnCreate(object Unknown = null) { }
        protected internal virtual void OnDelete(object Unknown = null) { }
        protected internal virtual void FixUpdate(object Unknown = null) { }
        protected internal virtual void OnCollide(Shape target) { }

        public Transform Transform
        {
            get => m_transform;
            set => m_transform = value;
        }

        public Shape Parent
        {
            get => m_parent;
            set => m_parent = value;
        }
        
        public Collider Collider
        {
            get => m_collider;
            set => m_collider = value;
        }
    }
}
