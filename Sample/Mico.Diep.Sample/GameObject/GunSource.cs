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
    abstract class GunSource : Shape
    {
        protected Vector2 g_shoot_position = new Vector2(0, 0);



        public Vector2 ShootPosition
        {
            get => g_shoot_position;
        }
    }
}
