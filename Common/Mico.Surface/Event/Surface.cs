using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Surface
{
    public partial class Surface
    {
        public virtual void OnLeftButtonDown() { }
        public virtual void OnRightButtonDown() { }
        public virtual void OnMiddleButtonDown() { }
        public virtual void OnLeftButtonUp() { }
        public virtual void OnRightButtonUp() { }
        public virtual void OnMiddleButtonUp() { }
        public virtual void OnMouseMove() { }
        public virtual void OnKeyDown() { }
        public virtual void OnKeyUp() { }
        public virtual void OnDraw() { }

    }
}
