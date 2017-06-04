using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Math;
using Mico.Shapes;
using Mico.Objects;

using Builder;
using Presenter;


namespace Mico.Collider.Sample
{
    public class Window : GenericWindow
    {

        public static int CubeCount => 50;
        public static int XLimit => 100;
        public static int YLimit => 100;
        public static int ZLimit => 100;

        static Random random = new Random();

        public static int INT => random.Next(1, 70);
        public static float FLOAT => (float)random.NextDouble();

        Surface surface;
        Shader vertexshader;
        Shader pixelshader;

        FpsCounter fps;

        public Window((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true);

            vertexshader = new VertexShader(@"ColliderVertexShader.hlsl", "main");
            pixelshader = new PixelShader(@"ColliderPixelShader.hlsl", "main");

            BufferLayout bufferlayout = new BufferLayout(
                new BufferLayout.Element[]
                {
                    new BufferLayout.Element() { Tag = "POSITION", Size = BufferLayout.ElementSize.eFloat3 },
                    new BufferLayout.Element() { Tag = "COLOR",    Size = BufferLayout.ElementSize.eFlaot4 }
                });

            Micos.Add(fps = new FpsCounter());

            Manager.Surface = surface;

            Manager.GraphicsPipelineState = new GraphicsPipelineState(vertexshader as VertexShader,
                pixelshader as PixelShader, bufferlayout);
          

            Micos.Camera = new Camera();
            Micos.Camera.Transform.Position = new Vector3(0, 0, -100);

            for (int i = 0; i < CubeCount; i++)
            {
                Cube cube = new Cube(10, 10, 10);
                cube.Transform.Position = new Vector3(INT, INT, INT);
                cube.Forward = Vector3.Normalize(new Vector3(INT, INT, 0));
                cube.RotateSpeed = new Vector3(FLOAT, FLOAT, 0);
                Micos.Add(cube);
            }

            Micos.Camera.Project = TMatrix.CreatePerspectiveFieldOfViewLH(
                   (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);

            IsVisible = true;
        }

        public override void OnMouseClick(object sender, MouseClickEventArgs e)
        {
            if (e.IsDown is true && e.Which is MouseButton.LeftButton)
            {
                Cube cube = Micos.Pick(Camera.NdcX(e.X, Width),
                  Camera.NdcY(e.Y, Height)) as Cube;

                if (cube is null) return;
                Micos.Remove(cube);
            }
        }

        public override void OnUpdate(object sender)
        {
            Manager.ClearObject();

            Micos.Exports();

            Fontface fontface = Fontface.Context[("Consolas", 12 * Manager.AppScale, 400)];
            Brush brush = Brush.Context[(0, 0, 0, 1)];

            Manager.PutText(fps.Fps.ToString(), (0, 0), brush, fontface);
            Manager.PutText("Click cube to destory it!", (0, fontface.Size), brush, fontface);

            Manager.FlushObject();

            Micos.Update();
        }

        public override void OnDestroyed(object sender)
        {
            Program.app.Destory();
        }

    }


}
