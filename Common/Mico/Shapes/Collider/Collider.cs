using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shapes;

namespace Mico.Shapes
{ 

    public abstract partial class Collider 
    {
        protected Shape g_parent = null;

        protected Type g_type = Type.UnKnown;

        TVector3 g_center = new TVector3(0, 0, 0);




        public TVector3 Center
        {
            get => g_center;
            set => g_center = value;
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
