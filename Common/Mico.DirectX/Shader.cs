using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.DirectX
{

    /// <summary>
    /// Shader
    /// </summary>
    public abstract partial class Shader
    {
        protected IntPtr source;



        public static implicit operator IntPtr(Shader shader)
            => shader.source;
    }
}
