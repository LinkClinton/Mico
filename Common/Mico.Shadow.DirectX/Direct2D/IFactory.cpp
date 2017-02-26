#include"..\pch.hpp"
 
void IFactoryCreate(ID2D1Factory1 ** source)
{
	D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,
		source);
}

void IFactoryDestory(ID2D1Factory1 * source)
{
	if (source == nullptr) return;
	source->Release();
}

void IFactoryGetDesktopDpi(ID2D1Factory1 * source, float * dpiX, float * dpiY)
{
	source->GetDesktopDpi(dpiX, dpiY);
}

auto IFactoryGetDevice(ID2D1Factory1* source, IDXGIDevice* dxgidevice)->ID2D1Device* 
{
	ID2D1Device* device = nullptr;

	source->CreateDevice(dxgidevice, &device);

	return device;
}

auto IFactoryGetRenderTarget(ID2D1Factory1* source, HWND hwnd)->ID2D1HwndRenderTarget*
{
	ID2D1HwndRenderTarget* rendertarget = nullptr;

	float dpiX;
	float dpiY;

	source->GetDesktopDpi(&dpiX, &dpiY);

	RECT rc;
	GetClientRect(hwnd, &rc);

	D2D1_SIZE_U size = D2D1::SizeU(rc.right - rc.left,
		rc.bottom - rc.top);

	source->CreateHwndRenderTarget(
		D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
			D2D1::PixelFormat(), dpiX, dpiY), D2D1::HwndRenderTargetProperties(hwnd,
				size), &rendertarget);

	return rendertarget;
}