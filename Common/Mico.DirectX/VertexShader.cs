using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{
    /// <summary>
    /// VertexShader based on Shader
    /// </summary>
    public class VertexShader : Shader
    {
        /// <summary>
        /// Create VertexShader
        /// </summary>
        /// <param name="Filename">Shader FileName</param>
        /// <param name="EntryPoint">Shader EntryPoint</param>
        /// <param name="IsCompiled">Is Shader Compiled. If not, We will compile it.</param>
        public VertexShader(string Filename, string EntryPoint, bool IsCompiled = false)
            => ShaderCreate(out source, Filename, EntryPoint,
               Type.eVertexShader, IsCompiled, Direct3D.Core); 
        

        

    }
}
