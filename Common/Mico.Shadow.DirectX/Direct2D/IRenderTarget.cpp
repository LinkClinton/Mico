#include"../pch.hpp"

typedef IRenderTarget This;

void IRenderTargetCreate(This** source, IFactory* factory, HWND hwnd) 
{
	float dpiX;
	float dpiY;

	*source = new IRenderTarget();

	factory->d2d1_factory->GetDesktopDpi(&dpiX, &dpiY);

	RECT rc;
	GetClientRect(hwnd, &rc);

	D2D1_SIZE_U size = D2D1::SizeU(rc.right - rc.left,
		rc.bottom - rc.top);

	HRESULT result =
		factory->d2d1_factory->CreateHwndRenderTarget(
			D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
				D2D1::PixelFormat(), dpiX, dpiY), D2D1::HwndRenderTargetProperties(hwnd,
					size), &(*source)->target);

	(*source)->factory = factory;
}

void IRenderTargetDestory(This* source) 
{
	if (source == nullptr) return;
	delete source;
}

void IRenderTargetClear(This* source, float r, float g, float b, float a = 1.0f) 
{
	source->target->Clear(D2D1::ColorF(r, g, b, a));
}

void IRenderTargetBeginDraw(This* source) 
{
	source->target->BeginDraw();
	source->target->Clear(D2D1::ColorF(D2D1::ColorF::White));
}

void IRenderTargetEndDraw(This* source) 
{
	source->target->EndDraw();
}

void IRenderTargetDrawLine(This* source, float x1, float y1, float x2, float y2,
	ID2D1Brush* brush, float width = 1.0f)
{
	source->target->DrawLine(D2D1::Point2F(x1, y1), D2D1::Point2F(x2, y2),
		brush, width);
}

void IRenderTargetDrawRectangle(This* source, float left, float top, float right, float bottom,
	ID2D1Brush* brush, float width = 1.0f)
{
	source->target->DrawRectangle(D2D1::RectF(left, top, right, bottom), brush,
		width);
}

void IRenderTargetFillRectangle(This* source, float left, float top, float right, float bottom,
	ID2D1Brush* brush)
{
	source->target->FillRectangle(D2D1::RectF(left, top, right, bottom), brush);
}

void IRenderTargetDrawText(This* source, float x, float y,
	LPCWSTR text, IDWriteTextFormat* font, ID2D1Brush* brush)
{
	IDWriteTextLayout* layout = nullptr;
	IDWriteFactory* factory = source->factory->write_factory;

	

	factory->CreateTextLayout(text, wcslen(text), font,
		INT16_MAX, INT16_MAX, &layout);

	source->target->DrawTextLayout(D2D1::Point2F(x, y), layout,
		brush);

	layout->Release();

}

void IRenderTargetDrawBitmap(This* source, float x, float y, ID2D1Bitmap* bitmap)
{
	D2D_SIZE_F size = bitmap->GetSize();

	source->target->DrawBitmap(bitmap,
		D2D1::RectF(x, y, x + size.width, y + size.height));
}

void IRenderTargetRotate(This* source, float x, float y, float angle)
{
	D2D1::Matrix3x2F transform;
	D2D1::Matrix3x2F matrix = D2D1::Matrix3x2F::Rotation(angle, D2D1::Point2F(x, y));

	source->target->GetTransform(&transform);

	matrix = matrix*transform;

	source->target->SetTransform(matrix);
}

void IRenderTargetTranslate(This* source, float x, float y)
{
	D2D1::Matrix3x2F transform;
	D2D1::Matrix3x2F matrix = D2D1::Matrix3x2F::Translation(D2D1::SizeF(x, y));

	source->target->GetTransform(&transform);

	matrix = matrix*transform;

	source->target->SetTransform(matrix);
}

void IRenderTargetScale(This* source, float x, float y, float scale_x, float scale_y)
{
	D2D1::Matrix3x2F transform;
	D2D1::Matrix3x2F matrix = D2D1::Matrix3x2F::Scale(D2D1::SizeF(scale_x, scale_y)
		, D2D1::Point2F(x, y));

	source->target->GetTransform(&transform);

	matrix = matrix*transform;

	source->target->SetTransform(matrix);
}

void IRenderTargetClearTransform(This* source)
{
	source->target->SetTransform(D2D1::Matrix3x2F::Identity());
}