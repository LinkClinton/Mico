using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample.GameResource
{
    public static class Brush
    {
        static IBrush g_black;

        public static void Initialize(IDevice device)
        {
            g_black = new IBrush(device, new TVector4(0, 0, 0, 1));
        }



        public static IBrush Black
        {
            get => g_black;
        }
    }
}
