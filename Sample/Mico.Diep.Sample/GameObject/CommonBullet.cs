using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico.Math;
using Mico.Shapes;
using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample.GameObject
{
    class CommonBullet : BulletSource
    {
        static float speed = 150;
        static TVector2 radius = new TVector2(10, 10);

        public CommonBullet(TankSource tank, Transform transform) :
            base(tank, transform)
        {
            
        }

        public override void OnUpdate(object Unknown = null)
        {
            Transform.Position += Transform.Forward * 
                (float)World.Time.DeltaTime.TotalSeconds * speed;

            base.OnUpdate(Unknown);
        }

        public override void OnExport(object Unknown = null)
        {
            IDevice device = Unknown as IDevice;

            device.Transform(Transform);

            device.RenderEllipse(new TVector2(0, 0), radius,
              GameResource.Brush.Black, 0.5f);

            device.Transform(Matrix3x2.Identity);

            base.OnExport(Unknown);
        }




    }
}
