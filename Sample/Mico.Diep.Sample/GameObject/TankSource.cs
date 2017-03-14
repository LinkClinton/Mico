using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


using Mico.Math;
using Mico.Shapes;
using Mico.World;
using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample.GameObject
{
    class TankSource : Shape
    {
        

        public TankSource()
        {

        }

        public override void OnRender(object Unknown = null)
        {
            IDevice device = Unknown as IDevice;

            device.Transform(Transform);
      

            device.Transform(Matrix3x2.Identity);

            base.OnRender(Unknown);
        }

        

    }
}
