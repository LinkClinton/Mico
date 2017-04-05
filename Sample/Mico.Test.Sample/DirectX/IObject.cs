using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Mico.DirectX;

namespace Mico.Test.Sample
{
    public class IObject
    {
        public Vertex[] vertex;
        public uint[] index;

        public Mico.DirectX.Buffer vertexbuffer;
        public Mico.DirectX.Buffer indexbuffer;

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

            for (int i = 0; i < result.vertex.Length; i++)
            {
                result.vertex[i].r = 1;
                result.vertex[i].g = 0;
                result.vertex[i].b = 0;
                result.vertex[i].a = 1;
            }

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
