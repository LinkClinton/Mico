using System;


namespace Mico
{
    public static partial class Input
    {

        public static bool GetKeyDown(Keycode keycode)
        {
            return IsKeyDown(keycode);
        }
        public static bool GetKeyUp(Keycode keycode)
        {
            return !IsKeyDown(keycode);
        }


    }
}
