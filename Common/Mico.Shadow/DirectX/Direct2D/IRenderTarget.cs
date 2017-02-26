using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX.Direct2D
{
    public partial class IRenderTarget
    {
        IntPtr source;

        public IRenderTarget(IFactory factory,IntPtr hwnd)
        {
            IRenderTargetCreate(out source, factory, hwnd);
        }

        ~IRenderTarget()
        {
            IRenderTargetDestory(source);
        }

        public void Clear(Vector4 color)
        {
            IRenderTargetClear(source, color.x, color.y, color.z, color.z);
        }

        public void BeginDraw()
        {
            IRenderTargetBeginDraw(source);
        }

        public void EndDraw()
        {
            IRenderTargetEndDraw(source);
        }

        public static implicit operator IntPtr(IRenderTarget rendertarget)
        {
            return rendertarget.source;
        }
    }
}
