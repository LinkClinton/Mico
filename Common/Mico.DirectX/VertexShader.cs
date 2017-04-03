using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    public class VertexShader : Shader
    {
        public VertexShader(string Filename, string EntryPoint, bool IsCompiled = false)
        {
            ShaderCreate(out source, Filename, EntryPoint,
               Type.eVertexShader, IsCompiled, Direct3D.Core);

            type = Type.eVertexShader;
        }

        ~VertexShader() => ShaderDestory(source);


    }
}
