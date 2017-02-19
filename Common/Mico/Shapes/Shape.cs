using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shapes
{
    public class Shape
    {
        Transform g_transform = new Transform();

        public virtual void OnUpdate(object Unknown = null) { }
        public virtual void OnRender(object Unknown = null) { }
        public virtual void OnCreate(object Unknown = null) { }
        public virtual void OnDelete(object Unknown = null) { }


        internal void FixUpdate()
        {
            g_transform.Position =
                g_transform.Position + g_transform.Forward * g_transform.Speed * (float)World.Time.DeltaTime.TotalSeconds;
        }


        public Transform Transform
        {
            set { g_transform = value; }
            get { return g_transform; }
        }
    }
}
