using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mico.Shadow.DirectX.Direct2D
{
    public partial class IBitmap
    {
        IntPtr source;

        public IBitmap(IRenderTarget target,string filename)
        {
            IBitmapCreate(out source, target, filename);
        }

        ~IBitmap()
        {
            IBitmapDestory(source);
        }

        float Width
        {
            get { return IBitmapGetWidth(source); }
        }

        float Height
        {
            get { return IBitmapGetHeight(source); }
        }

        public static implicit operator IntPtr(IBitmap bitmap)
        {
            return bitmap.source;
        }
    }
}
