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

            vertexshader = new VertexShader(@"..\..\Sample\Mico.Collider.Sample\shader.hlsl", "VSmain");
            pixelshader = new PixelShader(@"..\..\Sample\Mico.Collider.Sample\shader.hlsl", "PSmain");

            BufferLayout bufferlayout = new BufferLayout(
                new BufferLayout.Element[]
                {
                    new BufferLayout.Element() { Tag = "POSITION", Size = ElementSize.eFloat3 },
                    new BufferLayout.Element() { Tag = "COLOR",    Size = ElementSize.eFlaot4 }
                });

            ResourceLayout resourcelayout = new ResourceLayout(
                new ResourceLayout.Element[]
                {
                    new ResourceLayout.Element(ResourceType.ConstantBufferView,0),
                    new ResourceLayout.Element(ResourceType.ConstantBufferView,1)
                });

            Micos.Add(fps = new FpsCounter());

            Manager.Surface = surface;

            Manager.GraphicsPipelineState = new GraphicsPipelineState(vertexshader as VertexShader,
                pixelshader as PixelShader, bufferlayout, resourcelayout);
          

            Micos.Camera = new Camera();
            Micos.Camera.Transform.Position = new Vector3(0, 0, -100);

            /*for (int i = 0; i < CubeCount; i++)
            {
                Cube cube = new Cube(10, 10, 10);
                cube.Transform.Position = new Vector3(INT, INT, INT);
                cube.Forward = Vector3.Normalize(new Vector3(INT, INT, 0));
                cube.RotateSpeed = new Vector3(FLOAT, FLOAT, 0);
                Micos.Add(cube);
            }*/

            Cube cube1 = new Cube(10, 10, 10)
            {
                Color = new TVector4(1, 0, 0, 1), RotateSpeed = new Vector3(0, 0, 0),
                Speed = 0
            };
            cube1.Transform.Position = new Vector3(0, 0, 0);

            Cube cube2 = new Cube(10, 10, 10)
            {
                Color = new TVector4(0, 0, 0, 1),
                RotateSpeed = new Vector3(0, 0, 0),
                Speed = 0
            };

            cube2.Transform.Position = new Vector3(0, 0, 20);

            
            Micos.Add(cube2); Micos.Add(cube1);

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

            SetTitle(Program.AppTitle + " FPS: " + fps.FpsAverage);

            Manager.FlushObject();

            Micos.Update();
        }

        public override void OnDestroyed(object sender)
        {
            Program.app.Destory();
        }

    }


}
