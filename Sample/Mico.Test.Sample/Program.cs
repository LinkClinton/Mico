using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Test.Sample
{
    class Program
    {
        public static DirectX.TransformMatrix matrix = new DirectX.TransformMatrix();
        public static Mico.DirectX.ConstBuffer MatrixBuffer = new Mico.DirectX.ConstBuffer(matrix);

        static void Main(string[] args)
        {
            Window window = new Window();
            window.Run();
        }
    }
}
