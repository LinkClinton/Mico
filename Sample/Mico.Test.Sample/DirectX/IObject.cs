using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.InteropServices;

using Mico.Shapes;
using Mico.DirectX;

namespace Mico.Test.Sample
{
    public class IObject : Shape
    {
        protected override void OnUpdate(object Unknown = null)
        {
            Transform.Rotate = Quaternion.Multiply(Transform.Rotate, 
                Quaternion.CreateFromYawPitchRoll((float)World.Time.DeltaTime.TotalSeconds * 2.0f, 0, 0));

          

           
            base.OnUpdate(Unknown);
        }

        protected override void OnExport(object Unknown = null)
        {
            Program.matrix.view = Matrix4x4.Transpose(World.Micos.Camera);
            Program.matrix.world = Matrix4x4.Transpose(Transform);
            Program.MatrixBuffer.Update(Program.matrix);

            Direct3D.SetBufferToVertexShader(Program.MatrixBuffer, 0);
            Direct3D.SetBufferLayout(layout);
            Direct3D.SetBuffer(vertexbuffer);
            Direct3D.SetBuffer(indexbuffer);
            Direct3D.DrawIndexed(index.Length);
            base.OnExport(Unknown);
        }

        public Vertex[] vertex;
        public uint[] index;

        public Mico.DirectX.Buffer vertexbuffer;
        public Mico.DirectX.Buffer indexbuffer;
        public Mico.DirectX.BufferLayout layout;

        public IObject()
        {
            BufferLayout.Element[] element = new BufferLayout.Element[2];

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

            layout = new BufferLayout(element);
        }

        public static IObject CreateBox(int width, int height, int depth)
        {
            IObject result = new IObject();

            float w2 = 0.5f * width;
            float h2 = 0.5f * height;
            float d2 = 0.5f * depth;

            result.vertex = new Vertex[24];

            result.vertex[0] = new Vertex(-w2, -h2, -d2);
            result.vertex[1] = new Vertex(-w2, +h2, -d2);
            result.vertex[2] = new Vertex(+w2, +h2, -d2);
            result.vertex[3] = new Vertex(+w2, -h2, -d2);

            result.vertex[4] = new Vertex(-w2, -h2, +d2);
            result.vertex[5] = new Vertex(+w2, -h2, +d2);
            result.vertex[6] = new Vertex(+w2, +h2, +d2);
            result.vertex[7] = new Vertex(-w2, +h2, +d2);

            result.vertex[8] = new Vertex(-w2, +h2, -d2);
            result.vertex[9] = new Vertex(-w2, +h2, +d2);
            result.vertex[10] = new Vertex(+w2, +h2, +d2);
            result.vertex[11] = new Vertex(+w2, +h2, -d2);

            result.vertex[12] = new Vertex(-w2, -h2, -d2);
            result.vertex[13] = new Vertex(+w2, -h2, -d2);
            result.vertex[14] = new Vertex(+w2, -h2, +d2);
            result.vertex[15] = new Vertex(-w2, -h2, +d2);

            result.vertex[16] = new Vertex(-w2, -h2, +d2);
            result.vertex[17] = new Vertex(-w2, +h2, +d2);
            result.vertex[18] = new Vertex(-w2, +h2, -d2);
            result.vertex[19] = new Vertex(-w2, -h2, -d2);

            result.vertex[20] = new Vertex(+w2, -h2, -d2);
            result.vertex[21] = new Vertex(+w2, +h2, -d2);
            result.vertex[22] = new Vertex(+w2, +h2, +d2);
            result.vertex[23] = new Vertex(+w2, -h2, +d2);

            result.index = new uint[36]
            {
                 0,1,2,0,2,3,
                4,5,6,4,6,7,
                8,9,10,8,10,11,
                12,13,14,12,14,15,
                16,17,18,16,18,19,
                20,21,22,20,22,23
            };

            result.vertexbuffer = new VertexBuffer(result.vertex, result.vertex.Length, 28);
            result.indexbuffer = new IndexBuffer(result.index);
            
            return result;
        }
    }
}
