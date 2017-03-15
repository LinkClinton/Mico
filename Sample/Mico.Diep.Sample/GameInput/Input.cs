using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Diep.Sample.GameInput
{
    public static class Input
    {
        static TVector2 g_mousepos = new TVector2(0, 0);


        public static bool IsKeyDown(ConsoleKey key)
        {
            return Window.GetKeyState((int)key) < 0;
        }

        public static TVector2 MousePos
        {
            get => g_mousepos;
            set => g_mousepos = value;
        }

    }
}
