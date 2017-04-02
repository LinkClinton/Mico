#include"../pch.hpp"

IDirectXFont::~IDirectXFont() 
{
	release(source);
}

void IDirectXFontCreate(IDirectXFont** source, IDirectXDevice* device, LPCWSTR font,
	float size, int weight = 400)
{
	This = new IDirectXFont();

	HRESULT result;

	result = device->write_factory->CreateTextFormat(
		font, nullptr, (DWRITE_FONT_WEIGHT)weight,
		DWRITE_FONT_STYLE_NORMAL, DWRITE_FONT_STRETCH_NORMAL,
		size, L"Local", &This->source);

	DEBUG_LOG(result, DEBUG_DIRECT2D "Create Font failed");

}

void IDirectXFontDestory(IDirectXFont* source)
{
	if (source == nullptr) return;
	delete source;
}