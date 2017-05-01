#include "pch.hpp"

Manager::~Manager() 
{
	release(device2d);
	release(device3d);
	release(context3d);
	release(context3d);

	release(d2d1factory);
	release(imagefactory);
	release(writefactory);
}

void ManagerCreate(Manager** source)
{
	This = new Manager();

	D3D_FEATURE_LEVEL features[3] = {
		D3D_FEATURE_LEVEL_11_0,
		D3D_FEATURE_LEVEL_11_1,
		D3D_FEATURE_LEVEL_12_0
	};

	result = D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE,
		nullptr, D3D11_CREATE_DEVICE_BGRA_SUPPORT,
		features, 3, D3D11_SDK_VERSION, &This->device3d,
		&This->featureLevel, &This->context3d);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create Direct3D device failed");

	result = This->device3d->CheckMultisampleQualityLevels(DXGI_FORMAT_R8G8B8A8_UNORM,
		4, &This->msaa4xQuality);

	DEBUG_RESULT(DEBUG_DIRECT3D "Check MSAA4x quality failed");

	CoInitialize(nullptr);

	result = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,
		&This->d2d1factory);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create d2d1factory failed");

	result = CoCreateInstance(
		CLSID_WICImagingFactory, nullptr,
		CLSCTX_INPROC, IID_IWICImagingFactory,
		(void**)&This->imagefactory);

	DEBUG_RESULT(DEBUG_WIC "Create imagefactory failed");

	result = DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED,
		__uuidof(IDWriteFactory), (IUnknown**)&This->writefactory);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create writefactory failed");


	IDXGIDevice* dxgidevice = nullptr;

	result = This->device3d->QueryInterface(
		__uuidof(IDXGIDevice), (void**)&dxgidevice);

	DEBUG_RESULT(DEBUG_DIRECT3D "Query dxgifactory failed in create Manager");

	result = This->d2d1factory->CreateDevice(dxgidevice, &This->device2d);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create device2d failed");

	result = This->device2d->CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
		&This->context2d);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create context2d failed");

	result = This->d2d1factory->ReloadSystemMetrics();

	DEBUG_RESULT(DEBUG_DIRECT2D "Reload SystemMetrics failed");

	This->d2d1factory->GetDesktopDpi(&This->dpiX, &This->dpiY);

};

void ManagerDestory(Manager* source)
{
	if (source == nullptr) return;
	delete source;
}

void ManagerSetSurface(Manager* source, Surface* surface)
{
	DEBUG_BOOL(surface == nullptr, DEBUG_MANAGER "Surface is nullptr");

	This.currentChain = surface->swapchain;
	This.currentRTV = surface->surfaceRTV;
	This.currentDSV = surface->surfaceDSV;
	
	This.context3d->OMSetRenderTargets(1, &This.currentRTV,
		This.currentDSV);

	RECT rc;
	GetClientRect(surface->surfaceHWND, &rc);
	UINT width = rc.right - rc.left;
	UINT height = rc.bottom - rc.top;


	D3D11_VIEWPORT ViewPort = { 0 };
	ViewPort.Width = (float)(width);
	ViewPort.Height = (float)(height);
	ViewPort.MinDepth = 0.f;
	ViewPort.MaxDepth = 1.f;
	ViewPort.TopLeftX = 0.f;
	ViewPort.TopLeftY = 0.f;
	This.context3d->RSSetViewports(1, &ViewPort);

	ID3D11RasterizerState* rasterizer_state = nullptr;
	D3D11_RASTERIZER_DESC desc;

	desc.FillMode = D3D11_FILL_SOLID;
	desc.CullMode = D3D11_CULL_NONE;
	desc.FrontCounterClockwise = FALSE;
	desc.DepthBias = 0;
	desc.SlopeScaledDepthBias = 0.0f;
	desc.DepthBiasClamp = 0.0f;
	desc.DepthClipEnable = TRUE;
	desc.ScissorEnable = FALSE;
	desc.MultisampleEnable = FALSE;
	desc.AntialiasedLineEnable = FALSE;
	
	result = This.device3d->CreateRasterizerState(&desc, &rasterizer_state);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create RasterizerState failed");

	This.context3d->RSSetState(rasterizer_state);


	D2D1_BITMAP_PROPERTIES1 bitmapProperties =
		D2D1::BitmapProperties1(
			D2D1_BITMAP_OPTIONS_TARGET | D2D1_BITMAP_OPTIONS_CANNOT_DRAW,
			D2D1::PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED),
			This.dpiX,
			This.dpiY
		);

	IDXGISurface* Surface = nullptr;
	result = This.currentChain->GetBuffer(0, IID_PPV_ARGS(&Surface));

	DEBUG_RESULT(DEBUG_DXGI "SwapChain getbuffer failed");

	ID2D1Bitmap1* TargetBitmap = nullptr;

	result = This.context2d->CreateBitmapFromDxgiSurface(Surface,
		&bitmapProperties, &TargetBitmap);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create Bitmap to render failed");

	This.context2d->SetTarget(TargetBitmap);

}

void ManagerClear(Manager* source, D2D1::ColorF* color)
{
	float rgba[4] = { color->r,color->g,color->b,color->a };

	DEBUG_BOOL(This.currentRTV == nullptr, DEBUG_MANAGER "Clear failed, Surface's RTV is not set");
	DEBUG_BOOL(This.currentDSV == nullptr, DEBUG_MANAGER "Clear failed, Surface's DSV is not set");

	This.context2d->BeginDraw();
	This.context3d->ClearRenderTargetView(This.currentRTV, rgba);
	This.context3d->ClearDepthStencilView(This.currentDSV, D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);
}

void ManagerPresent(Manager* source)
{
	DEBUG_BOOL(This.currentChain == nullptr, DEBUG_MANAGER "Present failed, Surface is not set");

	result = This.context2d->EndDraw();

	DEBUG_RESULT(DEBUG_DIRECT2D "Present failed");

	result = This.currentChain->Present(0, 0);

	DEBUG_RESULT(DEBUG_DXGI "Present failed");
}

void ManagerDrawLine(Manager* source, D2D_POINT_2F* start,
	D2D_POINT_2F* end, Brush* brush, float width)
{
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "DrawLine failed, brush is nullptr");

	This.context2d->DrawLine(*start, *end, brush->brush, width);
}

void ManagerDrawRect(Manager* source, D2D_RECT_F* rect,
	Brush* brush, float width)
{
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "DrawRect failed, brush is nullptr");

	This.context2d->DrawRectangle(*rect, brush->brush, width);
}

void ManagerFillRect(Manager* source, D2D_RECT_F* rect,
	Brush* brush)
{
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "FillRect failed, brush is nullptr");

	This.context2d->FillRectangle(*rect, brush->brush);
}

void ManagerDrawEllipse(Manager* source, D2D_POINT_2F* center, D2D_POINT_2F* radius,
	Brush* brush, float width)
{
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "DrawEllipse failed, brush is nullptr");

	This.context2d->DrawEllipse(D2D1::Ellipse(*center, radius->x, radius->y),
		brush->brush, width);
}

void ManagerFillEllipse(Manager* source, D2D_POINT_2F* center, D2D_POINT_2F* radius,
	Brush* brush) 
{
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "FillEllipse failed, brush is nullptr");

	This.context2d->FillEllipse(D2D1::Ellipse(*center, radius->x, radius->y), brush->brush);
}

void ManagerDrawText(Manager* source, LPCWSTR text, D2D_POINT_2F* pos,
	Fontface* fontface, Brush* brush)
{
	DEBUG_BOOL(fontface == nullptr, DEBUG_MANAGER "DrawText failed, fontface is nullptr");
	DEBUG_BOOL(brush == nullptr, DEBUG_MANAGER "DrawText failed, brush is nullptr");

	IDWriteTextLayout* layout = nullptr;

	result = This.writefactory->CreateTextLayout(text, (UINT32)wcslen(text), fontface->fontface,
		INT16_MAX, INT16_MAX, &layout);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create TextInputLayout failed");

	This.context2d->DrawTextLayout(*pos, layout,
		brush->brush);

	layout->Release();

}

void ManagerDrawBitmap(Manager* source, D2D_RECT_F* rect,
	Bitmap* bitmap)
{
	DEBUG_BOOL(bitmap == nullptr, DEBUG_DIRECT2D "DrawBitmap failed, bitmap is nullptr");

	This.context2d->DrawBitmap(bitmap->bitmap,
		*rect);
}

void ManagerSetShader(Manager* source, Shader* shader)
{
	DEBUG_BOOL(shader == nullptr, "SetShader failed, shader is not vailed");

	switch (shader->shadertype)
	{
	case ShaderType::eVertexShader: {
		This.context3d->VSSetShader((ID3D11VertexShader*)shader->shader, nullptr, 0);
		This.currentVS = shader;
		break;
	}
	case ShaderType::ePixelShader: {
		This.context3d->PSSetShader((ID3D11PixelShader*)shader->shader, nullptr, 0);
		This.currentPS = shader;
		break;
	}
	default:
		break;
	}
}

void ManagerSetBuffer(Manager* source, Buffer* buffer, int bufferid , ShaderType type)
{
	DEBUG_BOOL(buffer->buffertype != BufferType::eConstBuffer, 
		DEBUG_MANAGER "SetBuffer failed, Don't support this type of buffer");

	switch (type)
	{
	case eVertexShader: {
		DEBUG_BOOL(This.currentVS->shader == nullptr,
			DEBUG_MANAGER "SetBuffer failed, VertexShader is not used");

		This.context3d->VSSetConstantBuffers(bufferid, 1, &buffer->buffer);
		break;
	}
	case ePixelShader:
		DEBUG_BOOL(This.currentPS->shader == nullptr,
			DEBUG_MANAGER "SetBuffer failed, PixelShader is not used");

		This.context3d->PSSetConstantBuffers(bufferid, 1, &buffer->buffer);
		break;
	default:
		break;
	}
}

void ManagerSetVertexBuffer(Manager* source, Buffer* buffer, int eachsize)
{
	UINT stride = eachsize;
	UINT offset = 0;
	This.context3d->IASetVertexBuffers(0, 1, &buffer->buffer, &stride, &offset);
}

void ManagerSetIndexBuffer(Manager* source, Buffer* buffer)
{
	This.context3d->IASetIndexBuffer(buffer->buffer, DXGI_FORMAT_R32_UINT, 0);
}

void ManagerSetBufferLayout(Manager* source, BufferLayout* layout)
{
	DEBUG_BOOL(layout == nullptr, DEBUG_MANAGER "Set BufferLayout failed, layout is nullptr");

	This.context3d->IASetInputLayout(layout->layout);
}

void ManagerDraw(Manager* source, int vertexcount, int startlocation, PrimitiveType type)
{
	This.context3d->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY(type));

	This.context3d->Draw(vertexcount, startlocation);
}

void ManagerDrawIndexed(Manager* source, int indexcount, int startlocation, int vertexlocation,
	PrimitiveType type)
{
	This.context3d->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY(type));
	
	This.context3d->DrawIndexed(indexcount, startlocation, vertexlocation);
}

void ManagerSetCullMode(Manager* source, D3D11_CULL_MODE mode)
{
	ID3D11RasterizerState* state = nullptr;
	D3D11_RASTERIZER_DESC desc = {};

	This.context3d->RSGetState(&state);
	state->GetDesc(&desc);

	desc.CullMode = mode;

	release(state);
	state = nullptr;

	result = This.device3d->CreateRasterizerState(&desc, &state);

	DEBUG_RESULT(DEBUG_DIRECT3D "CreateRasterizerState failed");

	This.context3d->RSSetState(state);
}

void ManagerSetFillMode(Manager* source, D3D11_FILL_MODE mode)
{
	ID3D11RasterizerState* state = nullptr;
	D3D11_RASTERIZER_DESC desc = {};

	This.context3d->RSGetState(&state);
	state->GetDesc(&desc);

	desc.FillMode = mode;

	release(state);
	state = nullptr;

	result = This.device3d->CreateRasterizerState(&desc, &state);

	DEBUG_RESULT(DEBUG_DIRECT3D "CreateRasterizerState failed");

	This.context3d->RSSetState(state);
}

void ManagerGetDpi(Manager* source, float* dpiX, float* dpiY)
{
	This.d2d1factory->GetDesktopDpi(dpiX, dpiY);
}

