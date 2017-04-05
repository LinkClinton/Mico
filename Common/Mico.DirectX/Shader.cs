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

        protected Type type;

        /// <summary>
        /// Shader Type
        /// </summary>
        public Type ShaderType { get => type; }

        public static implicit operator IntPtr(Shader shader)
            => shader.source;
    }
}
