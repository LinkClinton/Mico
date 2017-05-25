using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Shapes;
using Mico.Objects;

using Presenter;

namespace Mico.Cube.Sample
{
    public class IObject : Shape
    {
        protected override void OnUpdate(object Unknown = null)
        {

            Transform.Rotate = Quaternion.Multiply(Transform.Rotate,
             Quaternion.Inverse(Quaternion.CreateFromYawPitchRoll(Time.DeltaSeconds * 2.0f, 0, 0)));

            base.OnUpdate(Unknown);
        }

        protected override void OnExport(object Unknown = null)
        {
            Program.matrix.view = Micos.Camera;
            Program.matrix.world = Transform;
            Program.matrix.projection = Micos.Camera.Project;
            Program.MatrixBuffer.Update(ref Program.matrix);

            Manager.ConstantBuffer[(Manager.VertexShader, 0)] = Program.MatrixBuffer;
            Manager.BufferLayout = layout;
            Manager.VertexBuffer = vertexbuffer;
            Manager.IndexBuffer = indexbuffer;
            Manager.DrawObjectIndexed(index.Length);

            base.OnExport(Unknown);
        }

        public Vertex[] vertex;
        public uint[] index;

        public Presenter.Buffer vertexbuffer;
        public Presenter.Buffer indexbuffer;
        public Presenter.BufferLayout layout;

        public IObject()
        {
            BufferLayout.Element[] element = new BufferLayout.Element[3];

            element[0] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFloat3,
                Tag = "POSITION"
            };
            element[1] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFlaot4,
                Tag = "COLOR"
            };
            element[2] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFloat2,
                Tag = "TEXCOORD"
            };

            layout = new BufferLayout(element);
        }

        public static IObject CreateBox(float width, float height, float depth)
        {
            IObject result = new IObject();

            float w2 = 0.5f * width;
            float h2 = 0.5f * height;
            float d2 = 0.5f * depth;

            result.index = new uint[36] { 0,1,2,0,2,3,
                4,5,6,4,6,7,
                8,9,10,8,10,11,
                12,13,14,12,14,15,
                16,17,18,16,18,19,
                20,21,22,20,22,23 };

            result.vertex = new Vertex[24];

            result.vertex[0] = new Vertex(-w2, -h2, -d2, 0.0f, 1.0f);

            result.vertex[1] = new Vertex(-w2, +h2, -d2, 0.0f, 0.0f);

            result.vertex[2] = new Vertex(+w2, +h2, -d2, 1.0f, 0.0f);

            result.vertex[3] = new Vertex(+w2, -h2, -d2, 1.0f, 1.0f);



            result.vertex[4] = new Vertex(-w2, -h2, +d2, 1.0f, 1.0f);

            result.vertex[5] = new Vertex(+w2, -h2, +d2, 0.0f, 1.0f);

            result.vertex[6] = new Vertex(+w2, +h2, +d2, 0.0f, 0.0f);

            result.vertex[7] = new Vertex(-w2, +h2, +d2, 1.0f, 0.0f);



            result.vertex[8] = new Vertex(-w2, +h2, -d2, 0.0f, 1.0f);

            result.vertex[9] = new Vertex(-w2, +h2, +d2, 0.0f, 0.0f);

            result.vertex[10] = new Vertex(+w2, +h2, +d2, 1.0f, 0.0f);

            result.vertex[11] = new Vertex(+w2, +h2, -d2, 1.0f, 1.0f);



            result.vertex[12] = new Vertex(-w2, -h2, -d2, 1.0f, 1.0f);

            result.vertex[13] = new Vertex(+w2, -h2, -d2, 0.0f, 1.0f);

            result.vertex[14] = new Vertex(+w2, -h2, +d2, 0.0f, 0.0f);

            result.vertex[15] = new Vertex(-w2, -h2, +d2, 1.0f, 0.0f);



            result.vertex[16] = new Vertex(-w2, -h2, +d2, 0.0f, 1.0f);

            result.vertex[17] = new Vertex(-w2, +h2, +d2, 0.0f, 0.0f);

            result.vertex[18] = new Vertex(-w2, +h2, -d2, 1.0f, 0.0f);

            result.vertex[19] = new Vertex(-w2, -h2, -d2, 1.0f, 1.0f);



            result.vertex[20] = new Vertex(+w2, -h2, -d2, 0.0f, 1.0f);

            result.vertex[21] = new Vertex(+w2, +h2, -d2, 0.0f, 0.0f);

            result.vertex[22] = new Vertex(+w2, +h2, +d2, 1.0f, 0.0f);

            result.vertex[23] = new Vertex(+w2, -h2, +d2, 1.0f, 1.0f);

            result.vertexbuffer = new VertexBuffer<Vertex>(result.vertex);
            result.indexbuffer = new IndexBuffer<uint>(result.index);

            return result;
        }

        public static IObject CreateGrid(float width, float depth, uint dx, uint dz)
        {
            IObject result = new IObject();

            uint VertexCount = dx * dz;
            uint FaceCount = (dx - 1) * (dz - 1) * 2;

            float HalfWidth = 0.5f * width;
            float Halfdepth = 0.5f * depth;

            float Dx = width / (dx - 1);
            float Dz = depth / (dz - 1);

            float Du = 1.0f / (dx - 1);
            float Dv = 1.0f / (dz - 1);

            result.vertex = new Vertex[VertexCount];
            for (uint i = 0; i < dz; i++)
            {
                float z = Halfdepth - i * Dz;
                for (uint j = 0; j < dx; j++)
                {                 
                    float x = -HalfWidth + j * Dx;
                    uint index = i * dx + j;

                    result.vertex[index] = new Vertex(x, 0f, z, j * Du, i * Dv);
                }
            }

            int Index = 0;
            result.index = new uint[FaceCount * 3];



            for (uint i = 0; i < dz - 1; i++)
            {
                for (uint j = 0; j < dx - 1; j++)
                {

                    result.index[Index] = i * dx + j;
                    result.index[Index + 1] = i * dx + j + 1;
                    result.index[Index + 2] = (i + 1) * dx + j;

                    result.index[Index + 3] = (i + 1) * dx + j;
                    result.index[Index + 4] = i * dx + j + 1;
                    result.index[Index + 5] = (i + 1) * dx + j + 1;

                    Index += 6;
                }
            }

            result.indexbuffer = new IndexBuffer<uint>(result.index);
            result.vertexbuffer = new VertexBuffer<Vertex>(result.vertex);

            return result;
        }
    }
}
