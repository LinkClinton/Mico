﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Mico.Cube.Sample
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float x, y, z;
        public float r, g, b, a;
        public float u, v;

        public Vertex(float _x = 0, float _y = 0, float _z = 0, float _u = 0, float _v = 0)
        {
            x = _x;
            y = _y;
            z = _z;

            r = 0;
            g = 0;
            b = 0;
            a = 1;

            u = _u;
            v = _v;
        }
    }


}
