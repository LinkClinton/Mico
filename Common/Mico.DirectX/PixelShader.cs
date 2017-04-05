using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    /// <summary>
    /// PixelShader based on Shader
    /// </summary>
    public class PixelShader : Shader
    {

        /// <summary>
        /// Create PixelShader
        /// </summary>
        /// <param name="FileName">Shader FileName</param>
        /// <param name="EntryPoint">Shader EntryPoint</param>
        /// <param name="IsCompiled">Is Shader Compiled. If not, We will compile it.</param>
        public PixelShader(string FileName, string EntryPoint, bool IsCompiled = false)
        {
            ShaderCreate(out source, FileName, EntryPoint,
                  Type.ePixelShader, IsCompiled, Direct3D.Core);

            type = Type.ePixelShader;
        }


        ~PixelShader() => ShaderDestory(source);

    }
}
