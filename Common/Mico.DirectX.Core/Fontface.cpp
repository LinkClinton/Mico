#include "pch.hpp"

Fontface::~Fontface()
{
	release(fontface);
}

void FontfaceCreate(Fontface** source, LPCWSTR fontface, 
	float size, int weight, Manager* manger)
{
	This = new Fontface();

	result = manger->writefactory->CreateTextFormat(
		fontface, nullptr, (DWRITE_FONT_WEIGHT)weight,
		DWRITE_FONT_STYLE_NORMAL, DWRITE_FONT_STRETCH_NORMAL,
		size, L"Local", &This->fontface);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create fontface failed");
}

void FontfaceDestory(Fontface* source) 
{
	if (source == nullptr) return;
	delete source;
}