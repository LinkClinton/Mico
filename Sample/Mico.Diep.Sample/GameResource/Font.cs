using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample.GameResource
{
    public static class Font
    {
        static IFont g_consolas12;

        public static void Initialize(IDevice device)
        {
            g_consolas12 = new IFont(device, "Consolas", 12);
        }



        public static IFont Consolas12
        {
            get => g_consolas12;
        }
    }
}
