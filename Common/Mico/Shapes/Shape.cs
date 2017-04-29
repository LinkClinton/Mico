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
        Shape m_parent = null;

        Transform m_transform = new Transform();

        protected internal virtual void OnUpdate(object Unknown = null)
        {
            //
        }
        protected internal virtual void OnExport(object Unknown = null)
        {
            //
        }

        protected internal virtual void OnCreate(object Unknown = null)
        {
            //
        }

        protected internal virtual void OnDelete(object Unknown = null)
        {
            //
        }

        protected internal virtual void FixUpdate(object Unknown = null)
        {
           //
        }

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
        
    }
}
