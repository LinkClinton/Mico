using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX
{
    public partial class IDevice
    {
        IntPtr source;

        public IDevice(IntPtr hwnd, bool windowed = true) =>
            IDirectXDeviceCreate(out source, hwnd, windowed);

        ~IDevice() => IDirectXDeviceDestory(source);

        public void Clear(Vector4 color)
            => IDirectXDeviceClear(source, color);

        public void Present()
            => IDirectXDevicePresent(source);

        public void RenderLine(Vector2 start, Vector2 end,
            IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderLine(source, start, end, brush, width);

        public void RenderRect(Rect rect, IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderRect(source, rect, brush, width);

        public void FillRect(Rect rect, IBrush brush)
            => IDirectXDeviceFillRect(source, rect, brush);

        public void RenderEllipse(Vector2 center, Vector2 radius,
            IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderEllipse(source, center, radius, brush, width);

        public void FillEllipse(Vector2 center, Vector2 radius, IBrush brush)
            => IDirectXDeviceFillEllipse(source, center, radius, brush);

        public void RenderText(string text, Vector2 position,
            IFont font, IBrush brush)
            => IDirectXDeviceRenderText(source, text, position, font, brush);

        public void RenderBitmap(Rect rect, IBitmap bitmap)
            => IDirectXDeviceRenderBitmap(source, rect, bitmap);


        public static implicit operator IntPtr(IDevice device) 
            => device.source;

    }
}
