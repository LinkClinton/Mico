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

        public BoxTank()
        {

        }

        public override void OnRender(object Unknown = null)
        {
            IDevice device = Unknown as IDevice;

            device.Transform(Transform);

            device.RenderRect(g_rect, new IBrush(device, new TVector4(0, 0, 0, 1)), 0.5f);

            device.Transform(System.Numerics.Matrix3x2.Identity);

            base.OnRender(Unknown);
        }

        public override void OnUpdate(object Unknown = null)
        {   
            base.OnUpdate(Unknown);
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
