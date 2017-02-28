#include"../pch.hpp"

typedef ID2D1HwndRenderTarget This;

void IRenderTargetCreate(This** source, IFactory* factory, HWND hwnd) 
{
	float dpiX;
	float dpiY;

	factory->d2d1_factory->GetDesktopDpi(&dpiX, &dpiY);

	RECT rc;
	GetClientRect(hwnd, &rc);

	D2D1_SIZE_U size = D2D1::SizeU(rc.right - rc.left,
		rc.bottom - rc.top);

	factory->d2d1_factory->CreateHwndRenderTarget(
		D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
			D2D1::PixelFormat(), dpiX, dpiY), D2D1::HwndRenderTargetProperties(hwnd,
				size), source);
}

void IRenderTargetDestory(This* source) 
{
	if (source = nullptr) return;
	source->Release();
}

void IRenderTargetClear(This* source, float r, float g, float b, float a = 1.0f) 
{
	source->Clear(D2D1::ColorF(r, g, b, a));
}

void IRenderTargetBeginDraw(This* source) 
{
	source->BeginDraw();
}

void IRenderTargetEndDraw(This* source) 
{
	source->EndDraw();
}

void IRenderTargetDrawLine(This* source, float x1, float y1, float x2, float y2,
	ID2D1Brush* brush, float width = 1.0f)
{
	source->DrawLine(D2D1::Point2F(x1, y1), D2D1::Point2F(x2, y2),
		brush, width);
}

void IRenderTargetDrawRectangle(This* source, float left, float top, float right, float bottom,
	ID2D1Brush* brush, float width = 1.0f)
{
	source->DrawRectangle(D2D1::RectF(left, top, right, bottom), brush,
		width);
}

void IRenderTargetFillRectangle(This* source, float left, float top, float right, float bottom,
	ID2D1Brush* brush)
{
	source->FillRectangle(D2D1::RectF(left, top, right, bottom), brush);
}


