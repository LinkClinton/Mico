#include"../pch.hpp"

typedef ID2D1Brush This;

void IBrushCreate(This** source, ID2D1RenderTarget* target, float r, float g, float b, float a = 1.0f)
{
	ID2D1SolidColorBrush* brush = nullptr;

	target->CreateSolidColorBrush(D2D1::ColorF(r, g, b, a),
		&brush);

	source = (This**)&brush;
}

void IBrushDestory(This* source)
{
	if (source == nullptr) return;
	source->Release();
}



