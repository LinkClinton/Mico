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
        IFont  default_font;

        IntPtr source;

        public IRenderTarget(IFactory factory,IntPtr hwnd)
        {
            IRenderTargetCreate(out source, factory, hwnd);

            default_brush = new IBrush(this, new Vector4(0, 0, 0, 1));
            default_font = new IFont(this, "Consolas", 12);
        }

        ~IRenderTarget()
        {
            IRenderTargetDestory(source);
        }

        public void Clear(Vector4 color)
        {
            IRenderTargetClear(source, color.Red, color.Green, color.Blue, color.Alpha);
        }

        public bool Enable
        {
            set { if (value == true) IRenderTargetBeginDraw(source);
                else IRenderTargetEndDraw(source);
            }
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

        public void RenderText(string text, Vector2 postion, IFont font = null, IBrush brush = null)
        {
            if (brush == null) brush = default_brush;
            if (font == null) font = default_font;
            IRenderTargetDrawText(source, postion.x, postion.y, text, font, brush);
        }

        public void RenderBitmap(IBitmap bitmap, Vector2 position)
        {
            IRenderTargetDrawBitmap(source, position.x, position.y, bitmap);
        }

        public void Rotate(Vector2 center,float angle)
        {
            IRenderTargetRotate(source, center.x, center.y, angle);
        }

        public void Translate(Vector2 position)
        {
            IRenderTargetTranslate(source, position.x, position.y);
        }

        public void Scale(Vector2 position,Vector2 scale)
        {
            IRenderTargetScale(source, position.x, position.y, scale.x, scale.y);
        }

        public void ClearTransform()
        {
            IRenderTargetClearTransform(source);
        }


        public static implicit operator IntPtr(IRenderTarget rendertarget) => rendertarget.source;
    }
}
