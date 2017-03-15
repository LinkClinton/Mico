﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;
using Mico.Shapes;

namespace Mico.Objects
{
    public class SphereCollider : Collider
    {
        TVector3 g_radius = new TVector3(0, 0, 0);
       




        public SphereCollider(Shape shape)
        {
            g_parent = shape;
            g_type = Type.Sphere;
        }

        public TVector3 Radius
        {
            get => g_radius;
            set => g_radius = value;
        }

    }
}