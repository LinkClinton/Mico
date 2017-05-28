using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Mico.Math;
using Mico.Shapes;
using Mico.Objects;

using Builder;
using Presenter;

namespace Mico.Cube.Sample
{
    public class Window : GenericWindow
    {
        private Surface surface;
        private Shader vertexshader;
        private Shader pixelshader;
        private Texture texture;


        public Window((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true);

            vertexshader = new VertexShader(@"..\..\Sample\Mico.Cube.Sample\VertexShader.hlsl", "main");
            pixelshader = new PixelShader(@"..\..\Sample\Mico.Cube.Sample\PixelShader.hlsl", "main");
            texture = new Texture(@"..\..\Sample\Mico.Cube.Sample\Dream.png");

            Manager.Surface = surface;

            Manager.VertexShader = vertexshader as VertexShader;
            Manager.PixelShader = pixelshader as PixelShader;

            Manager.FillMode = FillMode.Solid;
            Manager.CullMode = CullMode.CullBack;

            PixelShader.Resource[0] = texture;

            Micos.Camera = new Camera();

            Micos.Add(IObject.CreateBox(4, 4, 4));

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;

            Micos.Camera.Project =
                 TMatrix.CreatePerspectiveFieldOfViewLH(
                     (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);

            Show();
        }

        public override void OnDestroyed(object sender)
        {
            base.OnDestroyed(sender);
            Program.app.Destory();
        }

        public override void OnUpdate(object sender)
        {
            Manager.ClearObject();

            Micos.Exports();

            Manager.FlushObject();

            Micos.Update();
        }
    }
}
