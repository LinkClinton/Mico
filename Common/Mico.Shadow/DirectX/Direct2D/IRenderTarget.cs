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
            IRenderTargetClear(source, color.Red, color.Green, color.Blue, color.Alpha);
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

        public void RenderRectangle(Rect rect, IBrush brush = null, float width = 1.0f)
        {
            if (brush == null) brush = default_brush;
            IRenderTargetDrawRectangle(source, rect.Left, rect.Top, rect.Right, rect.Bottom,
                brush, width);
        }

        public void FillRectangle(Rect rect, IBrush brush = null)
        {
            if (brush == null) brush = default_brush;
            IRenderTargetFillRectangle(source, rect.Left, rect.Top, rect.Right, rect.Bottom,
                brush);
        }

        public static implicit operator IntPtr(IRenderTarget rendertarget)
        {
            return rendertarget.source;
        }
    }
}
