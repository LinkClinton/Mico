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
    class BulletSource : Shape
    {
        public BulletSource(TankSource tank, Transform transform)
        {
            World.Micos.Add(this);

            Parent = tank;

            Transform = new Transform()
            {
                Position = transform.Position,
                Forward = transform.Forward
            };

            tank.Bullet.Add(this);
        }


        public virtual void Destory()
        {
            World.Micos.Remove(this);

            (Parent as TankSource).Bullet.Remove(this);
        }
    }
}
