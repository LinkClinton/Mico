using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;


namespace Mico.Collider.Sample
{
    class Program
    {

        public static TransformMatrix matrix = new TransformMatrix();
        public static TVector4 color = new TVector4(0, 0, 0, 1);
        public static Mico.DirectX.ConstBuffer MatrixBuffer = new Mico.DirectX.ConstBuffer(matrix);
        public static Mico.DirectX.ConstBuffer ColorBuffer = new Mico.DirectX.ConstBuffer(color);

        static void Main(string[] args)
        {
            Window window = new Window();
            window.Run();
        }
    }
}
