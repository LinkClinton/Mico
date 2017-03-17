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

        static bool g_left_mouse;
        static bool g_right_mouse;

        public static bool IsKeyDown(ConsoleKey key)
        {
            return Window.GetKeyState((int)key) < 0;
        }

        public static TVector2 MousePos
        {
            get => g_mousepos;
            set => g_mousepos = value;
        }

        public static bool LeftMouse
        {
            get => g_left_mouse;
            set => g_left_mouse = value;
        }

        public static bool RightMouse
        {
            get => g_right_mouse;
            set => g_right_mouse = value;
        }

    }
}
