using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    public class PixelShader : Shader
    {
        public PixelShader(string FileName, string EntryPoint, bool IsCompiled = false)
        {
            ShaderCreate(out source, FileName, EntryPoint,
                  Type.ePixelShader, IsCompiled, Direct3D.Core);

            type = Type.ePixelShader;
        }
        ~PixelShader() => ShaderDestory(source);

    }
}
