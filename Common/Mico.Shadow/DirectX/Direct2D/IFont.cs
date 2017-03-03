using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX.Direct2D
{
    public partial class IFont
    {
        IntPtr source;

        public IFont(IRenderTarget target, string fontface, float size, int weight = 400)
        {
            IFontCreate(out source, target, fontface,
                size, weight);
        }

        ~IFont()
        {
            IFontDestory(source);
        }

        public static implicit operator IntPtr(IFont font)
        {
            return font.source;
        }
    }
}
