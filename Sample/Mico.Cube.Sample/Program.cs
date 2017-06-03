using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Cube.Sample
{
    class Program
    {
        public static TransformMatrix matrix = new TransformMatrix();
        public static Presenter.ConstantBuffer<TransformMatrix> MatrixBuffer = new Presenter.ConstantBuffer<TransformMatrix>(matrix);
        public static Builder.GenericApplication app = new Builder.GenericApplication(null);


        static void Main(string[] args)
        {
            app.Add(new Window(("Mico.Cube.Sample", (int)(800 * Presenter.Manager.AppScale),
                (int)(600 * Presenter.Manager.AppScale))));

            app.RunLoop(60);

        }
    }
}
