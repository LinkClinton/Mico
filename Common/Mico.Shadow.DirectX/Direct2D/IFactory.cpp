#include"..\pch.hpp"
 



void IFactoryCreate(IFactory** source)
{
	*source = new IFactory();

	CoInitialize(nullptr);

	D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,
		&(*source)->d2d1_factory);

	CoCreateInstance(
		CLSID_WICImagingFactory,
		nullptr,
		CLSCTX_INPROC,
		IID_IWICImagingFactory,
		(void **)(&(*source)->iwic_factory));

	DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED,
		__uuidof(IDWriteFactory),
		(IUnknown**)(&(*source)->write_factory));
	
}

void IFactoryDestory(IFactory * source)
{
	if (source == nullptr) return;
	delete source;
}

void IFactoryGetDesktopDpi(IFactory * source, float * dpiX, float * dpiY)
{
	source->d2d1_factory->GetDesktopDpi(dpiX, dpiY);
}

auto IFactoryGetDevice(IFactory* source, IDXGIDevice* dxgidevice)->ID2D1Device*
{
	ID2D1Device* device = nullptr;

	source->d2d1_factory->CreateDevice(dxgidevice, &device);

	return device;
}

auto IFactoryGetRenderTarget(IFactory* source, HWND hwnd)->ID2D1HwndRenderTarget*
{
	ID2D1HwndRenderTarget* rendertarget = nullptr;

	float dpiX;
	float dpiY;

	source->d2d1_factory->GetDesktopDpi(&dpiX, &dpiY);

	RECT rc;
	GetClientRect(hwnd, &rc);

	D2D1_SIZE_U size = D2D1::SizeU(rc.right - rc.left,
		rc.bottom - rc.top);

	source->d2d1_factory->CreateHwndRenderTarget(
		D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
			D2D1::PixelFormat(), dpiX, dpiY), D2D1::HwndRenderTargetProperties(hwnd,
				size), &rendertarget);

	return rendertarget;
}