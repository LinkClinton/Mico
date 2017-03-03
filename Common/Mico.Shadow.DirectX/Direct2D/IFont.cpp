#include"..\pch.hpp"

typedef IDWriteTextFormat This;

void IFontCreate(This** source, IRenderTarget* target,
	LPCWSTR fontface, float size, int weight = 400) {

	HRESULT result = target->factory->write_factory->CreateTextFormat(fontface,
		nullptr, (DWRITE_FONT_WEIGHT)weight, DWRITE_FONT_STYLE_NORMAL,
		DWRITE_FONT_STRETCH_NORMAL, size, L"",
		source);
}

void IFontDestory(This* source) {
	if (source == nullptr) return;
	source->Release();
}