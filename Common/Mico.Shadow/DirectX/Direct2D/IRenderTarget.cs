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
        IBrush default_brush;

        IntPtr source;

        public IRenderTarget(IFactory factory,IntPtr hwnd)
        {
            IRenderTargetCreate(out source, factory, hwnd);

            default_brush = new IBrush(this, new Vector4(0, 0, 0, 1));
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

        public void RenderLine(Vector2 start, Vector2 end, IBrush brush = null, float width = 1.0f)
        {
            if (brush == null) brush = default_brush;
            IRenderTargetDrawLine(source, start.x, start.y, end.x, end.y, brush, width);
        }

        public static implicit operator IntPtr(IRenderTarget rendertarget)
        {
            return rendertarget.source;
        }
    }
}
