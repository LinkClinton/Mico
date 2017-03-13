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

        public void Clear(TVector4 color)
            => IDirectXDeviceClear(source, color);

        public void Present()
            => IDirectXDevicePresent(source);

        public void RenderLine(TVector2 start, TVector2 end,
            IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderLine(source, start, end, brush, width);

        public void RenderRect(TRect rect, IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderRect(source, rect, brush, width);

        public void FillRect(TRect rect, IBrush brush)
            => IDirectXDeviceFillRect(source, rect, brush);

        public void RenderEllipse(TVector2 center, TVector2 radius,
            IBrush brush, float width = 1.0f)
            => IDirectXDeviceRenderEllipse(source, center, radius, brush, width);

        public void FillEllipse(TVector2 center, TVector2 radius, IBrush brush)
            => IDirectXDeviceFillEllipse(source, center, radius, brush);

        public void RenderText(string text, TVector2 position,
            IFont font, IBrush brush)
            => IDirectXDeviceRenderText(source, text, position, font, brush);

        public void RenderBitmap(TRect rect, IBitmap bitmap)
            => IDirectXDeviceRenderBitmap(source, rect, bitmap);


        public static implicit operator IntPtr(IDevice device) 
            => device.source;

    }
}
