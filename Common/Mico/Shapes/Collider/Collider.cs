using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shapes;

namespace Mico.Shapes
{ 

    public abstract partial class Collider 
    {
        protected Shape g_parent = null;

        protected Type g_type = Type.UnKnown;

        protected Projection g_project = new Projection();

        Vector3 g_center = new Vector3(0, 0, 0);


        protected virtual void UpdateProjection()
        {

        }

        internal virtual bool IsCollide(Collider collider)
        {
            throw new Exception("Unknown Collider");
        }

        internal virtual void Update()
        {
            UpdateProjection();
        }

        public Vector3 LocalCenter
        {
            get => g_center;
            set => g_center = value;
        }

        public Vector3 GlobalCenter
        {
            get => Parent.Transform.Position + g_center;
        }
        
        public Shape Parent
        {
            get => g_parent;
        }

        public Type ColliderType
        {
            get => g_type;
        }

        public Transform Transform
        {
            get => Parent.Transform;
        }
        
    }
}
