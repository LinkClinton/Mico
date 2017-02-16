using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico
{
    public static partial class Input
    {
        internal static Vector2 g_mousepos;

        public static Vector2 MousePos
        {
            get { return g_mousepos; }
        }
    }
}
