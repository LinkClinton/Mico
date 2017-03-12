using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX
{
    public partial class IFont
    {
        IntPtr source = IntPtr.Zero;

        public IFont(IDevice device, string font, float size, int weight = 400)
            => IDirectXFontCreate(out source, device, font, size, weight); 

        ~IFont() => IDirectXFontDestory(source);



        public static implicit operator IntPtr(IFont font)
            => font.source;

    }
}
