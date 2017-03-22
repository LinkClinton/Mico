using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shadow.DirectX;

using Mico.Diep.Sample.GameInput;

namespace Mico.Diep.Sample.GameObject
{


    class BoxTank : TankSource
    {
        TVector2 g_radius = new TVector2(0, 0);
        TRect g_rect = new TRect(0, 0, 0, 0);
        CommonGun g_gun = null;


        public BoxTank()
        {
            g_gun = new CommonGun(this, new System.Numerics.Vector3(30, 0, 0));
        }

        public override void OnExport(object Unknown = null)
        {
            IDevice device = Unknown as IDevice;

            device.Transform(Transform);

            device.RenderRect(g_rect, GameResource.Brush.Black, 0.5f);

            device.Transform(System.Numerics.Matrix3x2.Identity);

            g_gun.OnExport(Unknown);

            base.OnExport(Unknown);
        }

        public override void OnUpdate(object Unknown = null)
        {

            if (IsPlayer is true && Input.LeftMouse is true)
                Shoot();
            base.OnUpdate(Unknown);
        }

        public override void Shoot()
        {
            new CommonBullet(this, new Shapes.Transform()
            {
                Forward = Transform.Forward,
                Position = new System.Numerics.Vector3(g_gun.ShootPosition, 0)
            });

            base.Shoot();
        }

        public TVector2 Radius
        {
            get => g_radius;
            set
            {
                g_radius = value;
                g_rect = new TRect(-g_radius.x, -g_radius.y, g_radius.x, g_radius.y);
            }
        }
    }
}
