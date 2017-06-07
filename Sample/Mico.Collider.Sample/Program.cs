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
        public static Presenter.ConstantBuffer<TransformMatrix> MatrixBuffer 
            = new Presenter.ConstantBuffer<TransformMatrix>(matrix);

        public static Builder.GenericApplication app = new Builder.GenericApplication(null);

        public static string AppTitle => "Mico.Collider.Sample";

        static void Main(string[] args)
        {
            app.Add(new Window((AppTitle, (int)(800 * Presenter.Manager.AppScale),
                (int)(600 * Presenter.Manager.AppScale))));
          
            app.RunLoop(0);

        }
    }
}
