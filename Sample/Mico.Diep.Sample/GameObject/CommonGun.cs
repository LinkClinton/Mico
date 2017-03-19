using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

using Mico.Shapes;
using Mico.Shadow.DirectX;


namespace Mico.Diep.Sample.GameObject
{
    class CommonGun : GunSource
    {

        public CommonGun(Shape parent, Vector3 position)
        {
            Parent = parent;

            Transform = new Transform()
            {
                Position = position
            };

            Matrix3x2 result = Matrix3x2.CreateTranslation(Transform.Position.X,
               Transform.Position.Y) * Parent.Transform;
            g_shoot_position = Vector2.Transform(Vector2.Zero, result);
        }

        //do not join the micos world.
        public override void OnRender(object Unknown = null)
        {
            IDevice device = Unknown as IDevice;

            Matrix3x2 result = Matrix3x2.CreateTranslation(Transform.Position.X,
                Transform.Position.Y) * Parent.Transform;

            g_shoot_position = Vector2.Transform(Vector2.Zero, result);
          
            device.Transform(result);

            device.RenderRect(new Math.TRect(-20, -10, 20, 10),
                GameResource.Brush.Black);

            device.Transform(Matrix3x2.Identity);


            base.OnRender(Unknown);
        }

    }
}
