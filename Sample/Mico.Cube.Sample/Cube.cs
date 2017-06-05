using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Shapes;

namespace Mico.Cube.Sample
{
    public class Cube : Shape
    {
        static Presenter.Buffer vertexbuffer;
        static Presenter.Buffer indexbuffer;

        static Cube()
        {
            var index = new uint[36] { 0,1,2,0,2,3,
                4,5,6,4,6,7,
                8,9,10,8,10,11,
                12,13,14,12,14,15,
                16,17,18,16,18,19,
                20,21,22,20,22,23
            };

            float w2 = 0.5f;
            float h2 = 0.5f;
            float d2 = 0.5f;

            var vertex = new Vertex[24];

            vertex[0] = new Vertex(-w2, -h2, -d2, 0.0f, 1.0f);
            vertex[1] = new Vertex(-w2, +h2, -d2, 0.0f, 0.0f);
            vertex[2] = new Vertex(+w2, +h2, -d2, 1.0f, 0.0f);

            vertex[3] = new Vertex(+w2, -h2, -d2, 1.0f, 1.0f);
            vertex[4] = new Vertex(-w2, -h2, +d2, 1.0f, 1.0f);
            vertex[5] = new Vertex(+w2, -h2, +d2, 0.0f, 1.0f);

            vertex[6] = new Vertex(+w2, +h2, +d2, 0.0f, 0.0f);
            vertex[7] = new Vertex(-w2, +h2, +d2, 1.0f, 0.0f);
            vertex[8] = new Vertex(-w2, +h2, -d2, 0.0f, 1.0f);

            vertex[9] = new Vertex(-w2, +h2, +d2, 0.0f, 0.0f);
            vertex[10] = new Vertex(+w2, +h2, +d2, 1.0f, 0.0f);
            vertex[11] = new Vertex(+w2, +h2, -d2, 1.0f, 1.0f);

            vertex[12] = new Vertex(-w2, -h2, -d2, 1.0f, 1.0f);
            vertex[13] = new Vertex(+w2, -h2, -d2, 0.0f, 1.0f);
            vertex[14] = new Vertex(+w2, -h2, +d2, 0.0f, 0.0f);

            vertex[15] = new Vertex(-w2, -h2, +d2, 1.0f, 0.0f);
            vertex[16] = new Vertex(-w2, -h2, +d2, 0.0f, 1.0f);
            vertex[17] = new Vertex(-w2, +h2, +d2, 0.0f, 0.0f);

            vertex[18] = new Vertex(-w2, +h2, -d2, 1.0f, 0.0f);
            vertex[19] = new Vertex(-w2, -h2, -d2, 1.0f, 1.0f);
            vertex[20] = new Vertex(+w2, -h2, -d2, 0.0f, 1.0f);

            vertex[21] = new Vertex(+w2, +h2, -d2, 0.0f, 0.0f);
            vertex[22] = new Vertex(+w2, +h2, +d2, 1.0f, 0.0f);
            vertex[23] = new Vertex(+w2, -h2, +d2, 1.0f, 1.0f);

            vertexbuffer = new Presenter.VertexBuffer<Vertex>(vertex);
            indexbuffer = new Presenter.IndexBuffer<uint>(index);
        }

        protected override void OnUpdate(object Unknown = null)
        {
            Transform.Rotate = System.Numerics.Quaternion.Multiply(Transform.Rotate,
                System.Numerics.Quaternion.Inverse(System.Numerics.Quaternion.CreateFromYawPitchRoll(Objects.Time.DeltaSeconds * 2.0f, 0, 0)));
        }

        protected override void OnExport(object Unknown = null)
        {
            Program.matrix.view = Micos.Camera;
            Program.matrix.world = Transform;
            Program.matrix.projection = Micos.Camera.Project;

            Program.MatrixBuffer.Update(ref Program.matrix);

            //Presenter.ResourceHeap heap = new Presenter.ResourceHeap(1);
            //heap.AddResource(Program.MatrixBuffer);

            Presenter.Manager.ResourceInput[0] = Program.MatrixBuffer;
            Presenter.Manager.ResourceInput[1] = Program.heap;
            Presenter.Manager.VertexBuffer = vertexbuffer;
            Presenter.Manager.IndexBuffer = indexbuffer;
            
            Presenter.Manager.DrawObjectIndexed(indexbuffer.Count);

        }

        public Cube(float width,float height,float depth)
        {
            Transform.Scale = new System.Numerics.Vector3(width, height, depth);
        }

    }
}
