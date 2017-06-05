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

        public static Presenter.Texture texture = new Presenter.Texture(@"..\..\Sample\Mico.Cube.Sample\Dream.png");
        public static Presenter.ResourceHeap heap = new Presenter.ResourceHeap(1);

        static void Main(string[] args)
        {
            heap.AddResource(texture);
            app.Add(new Window(("Mico.Cube.Sample", (int)(800 * Presenter.Manager.AppScale),
                (int)(600 * Presenter.Manager.AppScale))));

            app.RunLoop(60);

        }
    }
}
