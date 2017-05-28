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
        public static Presenter.ConstantBuffer<TransformMatrix> MatrixBuffer 
            = new Presenter.ConstantBuffer<TransformMatrix>(matrix);

        public static Presenter.ConstantBuffer<TVector4> ColorBuffer
            = new Presenter.ConstantBuffer<TVector4>(color);

        public static Builder.GenericApplication app = new Builder.GenericApplication(null);

        static void Main(string[] args)
        {
            app.Add(new Window(("Mico.Collider.Sample", (int)(800 * Presenter.Manager.AppScale),
                (int)(600 * Presenter.Manager.AppScale))));

            app.RunLoop(60);

        }
    }
}
