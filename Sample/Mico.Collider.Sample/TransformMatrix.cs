using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Numerics;


namespace Mico.Collider.Sample
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TransformMatrix
    {
        public Matrix4x4 world;
        public Matrix4x4 view;
        public Matrix4x4 projection;
    }



}
