#include "pch.hpp"

Surface::~Surface()
{
	release(swapchain);

	release(surfaceRTV);
	release(surfaceDSV);
}

void SurfaceCreate(Surface** source, HWND hwnd, bool windowed, Manager* manager)
{
	This = new Surface();

	RECT rc;
	GetClientRect(hwnd, &rc);
	UINT width = rc.right - rc.left;
	UINT height = rc.bottom - rc.top;


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
	chain_desc.SampleDesc.Quality = manager->msaa4xQuality - 1;
	chain_desc.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;
	chain_desc.Windowed = windowed;

	IDXGIDevice* dxgidevice = nullptr;
	IDXGIAdapter* dxgiadapter = nullptr;
	IDXGIFactory* dxgifactory = nullptr;
	
	
	result = manager->device3d->QueryInterface(
		__uuidof(IDXGIDevice), reinterpret_cast<void**>(&dxgidevice));

	DEBUG_RESULT(DEBUG_DIRECT3D "Query DXGIDevice Failed");

	result = dxgidevice->GetParent(
		__uuidof(IDXGIAdapter), reinterpret_cast<void**>(&dxgiadapter));

	DEBUG_RESULT(DEBUG_DXGI "Get DXGIAdapter failed");

	result = dxgiadapter->GetParent(
		__uuidof(IDXGIFactory), reinterpret_cast<void**>(&dxgifactory));

	DEBUG_RESULT(DEBUG_DXGI "Get DXGIFactory failed");


	result = dxgifactory->CreateSwapChain(manager->device3d,
		&chain_desc, &This->swapchain);

	DEBUG_RESULT(DEBUG_DXGI "Create SwapChain failed");

	ID3D11Texture2D* backbuffer = nullptr;

	result = This->swapchain->GetBuffer(0, __uuidof(ID3D11Texture2D), reinterpret_cast<void**>(&backbuffer));

	DEBUG_RESULT(DEBUG_DXGI "Get BackBuffer failed");

	result = manager->device3d->CreateRenderTargetView(backbuffer, nullptr,
		&This->surfaceRTV);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create RenderTargetView failed");

	D3D11_TEXTURE2D_DESC depth_desc = { 0 };
	depth_desc.Width = width;
	depth_desc.Height = height;
	depth_desc.MipLevels = 1;
	depth_desc.ArraySize = 1;
	depth_desc.BindFlags = D3D11_BIND_DEPTH_STENCIL;
	depth_desc.Format = DXGI_FORMAT_D24_UNORM_S8_UINT;
	depth_desc.MiscFlags = 0;
	depth_desc.SampleDesc.Count = 4;
	depth_desc.SampleDesc.Quality = manager->msaa4xQuality - 1;
	depth_desc.Usage = D3D11_USAGE_DEFAULT;
	depth_desc.CPUAccessFlags = 0;

	ID3D11Texture2D* depth_buffer = nullptr;


	result = manager->device3d->CreateTexture2D(&depth_desc,
		nullptr, &depth_buffer);


	DEBUG_RESULT(DEBUG_DIRECT3D "Create DepthBuffer failed");

	result = manager->device3d->CreateDepthStencilView(depth_buffer,
		nullptr, &This->surfaceDSV);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create DepthStencilView failed");

	This->surfaceHWND = hwnd;

	D2D1_BITMAP_PROPERTIES1 bitmapProperties =
		D2D1::BitmapProperties1(
			D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
			D2D1::PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED),
			manager->dpiX,
			manager->dpiY
		);

	IDXGISurface* Surface = nullptr;
	result = This->swapchain->GetBuffer(0, IID_PPV_ARGS(&Surface));

	DEBUG_RESULT(DEBUG_DXGI "SwapChain getbuffer failed");

	result = manager->context2d->CreateBitmapFromDxgiSurface(Surface,
		&bitmapProperties, &This->surfaceTarget);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create Bitmap to render failed");
}

void SurfaceResize(Surface* source, float width, float height)
{
	This.swapchain->ResizeBuffers(1, (UINT)width, (UINT)height, DXGI_FORMAT_R8G8B8A8_UNORM, 0);
	throw "WTF";
	//Need Fix
}

void SurfaceDestory(Surface* source) 
{
	if (source == nullptr) return;
	delete source;
}

