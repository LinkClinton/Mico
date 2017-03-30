#include"../pch.hpp"



IDirectXDevice::~IDirectXDevice() {
	release(d2d1_factory);
	release(write_factory);
	release(image_factory);
	
	release(device2d);
	release(context2d);

	release(device3d);
	release(context3d);
	release(targetview);
	release(depthview);

	release(chain);
}

void IDirectXDeviceCreate(IDirectXDevice** source, HWND hwnd, bool windowed = true)
{
	This = new IDirectXDevice();

	//Windows 
	RECT rc;
	GetClientRect(hwnd, &rc);
	UINT width = rc.right - rc.left;
	UINT height = rc.bottom - rc.top;

	This->width = (float)width;
	This->height = (float)height;

	//Direct2D Factory
	CoInitialize(nullptr);

	D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,
		&This->d2d1_factory);

	CoCreateInstance(
		CLSID_WICImagingFactory, nullptr,
		CLSCTX_INPROC, IID_IWICImagingFactory,
		(void**)&This->image_factory);

	DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED,
		__uuidof(IDWriteFactory), (IUnknown**)&This->write_factory);

	//Direct3D 
	D3D_FEATURE_LEVEL features[3] = {
		D3D_FEATURE_LEVEL_11_0,
		D3D_FEATURE_LEVEL_11_1,
		D3D_FEATURE_LEVEL_12_0
	};

	D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE,
		nullptr, D3D11_CREATE_DEVICE_BGRA_SUPPORT,
		features, 3, D3D11_SDK_VERSION, &This->device3d,
		&This->feature, &This->context3d);

	This->device3d->CheckMultisampleQualityLevels(
		DXGI_FORMAT_R8G8B8A8_UNORM, 4, &This->MSAA4xQuality);






	DXGI_SWAP_CHAIN_DESC chain_desc;
	chain_desc.BufferDesc.Width = width;
	chain_desc.BufferDesc.Height = height;
	chain_desc.BufferDesc.RefreshRate.Denominator = 1;
	chain_desc.BufferDesc.RefreshRate.Numerator = 60;
	chain_desc.BufferDesc.Scaling = DXGI_MODE_SCALING_UNSPECIFIED;
	chain_desc.BufferDesc.ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED;
	chain_desc.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	chain_desc.BufferCount = 1;
	chain_desc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	chain_desc.Flags = 0;
	chain_desc.OutputWindow = hwnd;
	chain_desc.SampleDesc.Count = 4;
	chain_desc.SampleDesc.Quality = This->MSAA4xQuality - 1;
	chain_desc.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;
	chain_desc.Windowed = windowed;

	

	IDXGIDevice* dxgidevice = nullptr;
	IDXGIAdapter* dxgiadapter = nullptr;
	IDXGIFactory* dxgifactory = nullptr;

	This->device3d->QueryInterface(
		__uuidof(IDXGIDevice), (void**)&dxgidevice);
	dxgidevice->GetParent(
		__uuidof(IDXGIAdapter), (void**)&dxgiadapter);
	dxgiadapter->GetParent(
		__uuidof(IDXGIFactory), (void**)&dxgifactory);

	dxgifactory->CreateSwapChain(This->device3d,
		&chain_desc, &This->chain);


	ID3D11Texture2D* backbuffer = nullptr;
	
	This->chain->GetBuffer(0, __uuidof(ID3D11Texture2D), (void**)&backbuffer);

	This->device3d->CreateRenderTargetView(backbuffer, nullptr,
		&This->targetview);

	D3D11_TEXTURE2D_DESC depth_desc = { 0 };
	depth_desc.Width = width;
	depth_desc.Height = height;
	depth_desc.MipLevels = 1;
	depth_desc.ArraySize = 1;
	depth_desc.BindFlags = D3D11_BIND_DEPTH_STENCIL;
	depth_desc.Format = DXGI_FORMAT_D24_UNORM_S8_UINT;
	depth_desc.MiscFlags = 0;
	depth_desc.SampleDesc.Count = 4;
	depth_desc.SampleDesc.Quality = This->MSAA4xQuality - 1;
	depth_desc.Usage = D3D11_USAGE_DEFAULT;
	depth_desc.CPUAccessFlags = 0;
	
	ID3D11Texture2D* depth_buffer = nullptr;


	This->device3d->CreateTexture2D(&depth_desc,
		nullptr, &depth_buffer);

	This->device3d->CreateDepthStencilView(depth_buffer,
		nullptr, &This->depthview);

	This->context3d->OMSetRenderTargets(1, &This->targetview,
		This->depthview);

	D3D11_VIEWPORT ViewPort = { 0 };
	ViewPort.Width = (float)(width);
	ViewPort.Height = (float)(height);
	ViewPort.MinDepth = 0.f;
	ViewPort.MaxDepth = 1.f;
	ViewPort.TopLeftX = 0.f;
	ViewPort.TopLeftY = 0.f;
	This->context3d->RSSetViewports(1, &ViewPort);

	IDXGISurface* Surface = nullptr;
	This->chain->GetBuffer(0, IID_PPV_ARGS(&Surface));

	This->d2d1_factory->ReloadSystemMetrics();
	This->d2d1_factory->GetDesktopDpi(
		&This->dpiX, &This->dpiY);

	This->d2d1_factory->CreateDevice(dxgidevice,
		&This->device2d);

	This->device2d->CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
		&This->context2d);

	D2D1_BITMAP_PROPERTIES1 bitmapProperties =
		D2D1::BitmapProperties1(
			D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
			D2D1::PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED),
			This->dpiX,
			This->dpiY
		);

	ID2D1Bitmap1* TargetBitmap = nullptr;


	This->context2d->CreateBitmapFromDxgiSurface(Surface,
		&bitmapProperties, &TargetBitmap);

	This->context2d->SetTarget(TargetBitmap);
	

}

void IDirectXDeviceDestory(IDirectXDevice* source) 
{
	if (source == nullptr) return;
	delete source;
}

void IDirectXDeviceClear(IDirectXDevice* source, D2D1::ColorF* color)
{
	float rgba[4] = { color->r,color->g,color->b,color->a };
	This.context3d->ClearRenderTargetView(This.targetview, rgba);
	This.context3d->ClearDepthStencilView(This.depthview, D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);
	This.context2d->BeginDraw();
}

void IDirectXDevicePresent(IDirectXDevice* source)
{
	This.context2d->EndDraw();
	This.chain->Present(0, 0);
}

void IDirectXDeviceRenderLine(IDirectXDevice* source, D2D_POINT_2F* start,
	D2D_POINT_2F* end, IDirectXBrush* brush, float width = 1.0f)
{

	source->context2d->DrawLine(*start, *end, 
		brush->source, width);
}

void IDirectXDeviceRenderRect(IDirectXDevice* source, D2D_RECT_F* rect,
	IDirectXBrush* brush, float width = 1.0f)
{
	This.context2d->DrawRectangle(*rect, brush->source, width);
}

void IDirectXDeviceFillRect(IDirectXDevice* source, D2D_RECT_F* rect,
	IDirectXBrush* brush)
{
	This.context2d->FillRectangle(*rect, brush->source);
}

void IDirectXDeviceRenderEllipse(IDirectXDevice* source, D2D_POINT_2F* center,
	D2D_POINT_2F* radius, IDirectXBrush* brush, float width = 1.0f)
{
	This.context2d->DrawEllipse(D2D1::Ellipse(*center, radius->x, radius->y), brush->source, width);
}

void IDirectXDeviceFillEllipse(IDirectXDevice* source, D2D_POINT_2F* center,
	D2D_POINT_2F* radius, IDirectXBrush* brush)
{
	This.context2d->FillEllipse(D2D1::Ellipse(*center, 
		radius->x, radius->y), brush->source);
}

void IDirectXDeviceRenderText(IDirectXDevice* source, LPCWSTR text,
	D2D_POINT_2F* pos, IDirectXFont* font, IDirectXBrush* brush)
{

	IDWriteTextLayout* layout = nullptr;

	This.write_factory->CreateTextLayout(text, (UINT32)wcslen(text), font->source,
		INT16_MAX, INT16_MAX, &layout);

	This.context2d->DrawTextLayout(*pos, layout,
		brush->source);

	layout->Release();
}

void IDirectXDeviceRenderBitmap(IDirectXDevice* source, D2D_RECT_F* rect,
	IDirectXBitmap* bitmap)
{
	source->context2d->DrawBitmap(bitmap->source,
		*rect);
}

void IDirectXDeviceTransform(IDirectXDevice* source, D2D1::Matrix3x2F matrix)
{
	source->context2d->SetTransform(matrix);
}