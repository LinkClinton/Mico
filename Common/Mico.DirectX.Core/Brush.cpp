#include "pch.hpp"

Brush::~Brush()
{
	release(brush);
}

void BrushCreate(Brush** source, D2D1::ColorF* color, Manager* manager)
{
	This = new Brush();

	ID2D1SolidColorBrush* brush = nullptr;

	result = manager->context2d->CreateSolidColorBrush(*color, &brush);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create SolidBrush failed");

	This->brush = brush;
}

void BrushDestory(Brush* source)
{
	if (source == nullptr) return;
	delete source;
}
