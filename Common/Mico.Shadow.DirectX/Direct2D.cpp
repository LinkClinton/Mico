#include "Direct2D.hpp"


#pragma comment(lib,"d2d1.lib")
#pragma comment(lib,"user32.lib")

auto Mico::Shadow::DirectX::Direct2D::IFactory::Create() -> IntPtr
{
	ID2D1Factory1* factory = nullptr;

	HRESULT result = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,
		&factory);

	DEBUG_LOG(result, "Create Direct2D Factory Failed");

	return (IntPtr)factory;
}

void Mico::Shadow::DirectX::Direct2D::IFactory::Destory(IntPtr source)
{
	ID2D1Factory1* factory = (ID2D1Factory1*)source.ToPointer();
	if (factory != nullptr)
		factory->Release();
}

auto Mico::Shadow::DirectX::Direct2D::IFactory::GetDesktopDpi(IntPtr source) -> Math::Vector2^
{
	float dpiX; 
	float dpiY;
	ID2D1Factory1* factory = (ID2D1Factory1*)source.ToPointer();
	
	factory->ReloadSystemMetrics();
	factory->GetDesktopDpi(&dpiX, &dpiY);

	return gcnew Math::Vector2(dpiX, dpiY);
}

auto Mico::Shadow::DirectX::Direct2D::IFactory::GetDevice(IntPtr source, IntPtr dxgidevice) -> IntPtr
{
	ID2D1Device* device = nullptr;
	ID2D1Factory1* factory = (ID2D1Factory1*)source.ToPointer();

	HRESULT result = 
		factory->CreateDevice((IDXGIDevice*)dxgidevice.ToPointer(), &device);
	
	DEBUG_LOG(result, "Create Direct2D Device Failed");

	return (IntPtr)device;
}

auto Mico::Shadow::DirectX::Direct2D::IFactory::GetRenderTarget(IntPtr source, IntPtr hwnd) -> IntPtr
{
	ID2D1Factory1* factory = (ID2D1Factory1*)source.ToPointer();
	ID2D1HwndRenderTarget* rendertarget;

	float dpiX;
	float dpiY;

	factory->GetDesktopDpi(&dpiX, &dpiY);

	RECT rc;
	GetClientRect((HWND)hwnd.ToPointer(), &rc);

	D2D1_SIZE_U size = D2D1::SizeU(rc.right - rc.left,
		rc.bottom - rc.top);

	HRESULT result = factory->CreateHwndRenderTarget(
		D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
			D2D1::PixelFormat(), dpiX, dpiY), D2D1::HwndRenderTargetProperties((HWND)hwnd.ToPointer(),
				size), &rendertarget);

	return (IntPtr)rendertarget;
}

auto Mico::Shadow::DirectX::Direct2D::IDevice::Create(IntPtr factory, IntPtr dxgidevice) -> IntPtr
{
	return IFactory::GetDevice(factory, dxgidevice);
}

void Mico::Shadow::DirectX::Direct2D::IDevice::Destory(IntPtr source)
{
	ID2D1Device* device = (ID2D1Device*)source.ToPointer();

	if (device != nullptr)
		device->Release();
}

auto Mico::Shadow::DirectX::Direct2D::IDevice::GetDeviceContext(IntPtr source) -> IntPtr
{
	ID2D1Device* device = (ID2D1Device*)source.ToPointer();
	ID2D1DeviceContext* devicecontext;

	HRESULT result = device->CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS_NONE, &devicecontext);

	DEBUG_LOG(result, "Create DeviceContext Failed");

	return (IntPtr)devicecontext;
}

auto Mico::Shadow::DirectX::Direct2D::IDeviceContext::Create(IntPtr device) -> IntPtr
{
	return IDevice::GetDeviceContext(device);
}

void Mico::Shadow::DirectX::Direct2D::IDeviceContext::Destory(IntPtr source)
{
	ID2D1DeviceContext* devicecontext = (ID2D1DeviceContext*)source.ToPointer();
	
	if (devicecontext != nullptr)
		devicecontext->Release();

}

void Mico::Shadow::DirectX::Direct2D::IDeviceContext::SetTarget(IntPtr source, IntPtr bitmap)
{
	ID2D1DeviceContext* devicecontext = (ID2D1DeviceContext*)source.ToPointer();

	devicecontext->SetTarget((ID2D1Bitmap1*)bitmap.ToPointer());

}

auto Mico::Shadow::DirectX::Direct2D::IDeviceContext::GetBitmapFromDXGISurface(IntPtr source, IntPtr dxgisurface) -> IntPtr
{
	IntPtr factory = IFactory::Create();
	Mico::Math::Vector2^ dpi = IFactory::GetDesktopDpi(factory);
	IFactory::Destory(factory);

	D2D1_BITMAP_PROPERTIES1 bitmapProperties =
		D2D1::BitmapProperties1(
			D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
			D2D1::PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED),
			dpi->x,
			dpi->y
		);

	ID2D1Bitmap1* bitmap = nullptr;
	ID2D1DeviceContext* devicecontext = (ID2D1DeviceContext*)source.ToPointer();
	
	HRESULT result = devicecontext->CreateBitmapFromDxgiSurface((IDXGISurface*)dxgisurface.ToPointer(),
		&bitmapProperties, &bitmap);

	DEBUG_LOG(result, "Create Bitmap From Surface Failed");

	return (IntPtr)bitmap;
}

auto Mico::Shadow::DirectX::Direct2D::IRenderTarget::Create(IntPtr factory, IntPtr hwnd) -> IntPtr
{
	return IFactory::GetRenderTarget(factory, hwnd);
}

void Mico::Shadow::DirectX::Direct2D::IRenderTarget::Destory(IntPtr source)
{
	ID2D1HwndRenderTarget* rendertarget = (ID2D1HwndRenderTarget*)source.ToPointer();

	if (rendertarget != nullptr)
		rendertarget->Release();
}
