using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX
{
    public partial class IShader
    {
        IntPtr source;

        public IShader(string filename, string entrypoint, Type type)
            => IDirectXShaderCreate(out source, filename, entrypoint, type);
    
        ~IShader()
            => IDirectXShaderDestory(source);

        public void Compile() => IDirectXShaderCompile(source);


        public static implicit operator IntPtr(IShader shader)
            => shader.source;

    }
}
