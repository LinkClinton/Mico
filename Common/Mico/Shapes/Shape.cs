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

        internal virtual void OnUpdate(object Unknown = null)
        {
            //
        }
        internal virtual void OnExport(object Unknown = null)
        {
            //
        }

        internal virtual void OnCreate(object Unknown = null)
        {
            //
        }

        internal virtual void OnDelete(object Unknown = null)
        {
            //
        }

        internal virtual void FixUpdate(object Unknown = null)
        {
           //
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
