#include"../pch.hpp"


IDirectXBrush::~IDirectXBrush() {
	release(source);
}

void IDirectXBrushCreate(IDirectXBrush** source, IDirectXDevice* device, D2D1::ColorF* color)
{
	This = new IDirectXBrush();

	ID2D1SolidColorBrush* brush = nullptr;

	HRESULT result = device->context2d->CreateSolidColorBrush(*color, &brush);

	DEBUG_LOG(result, DEBUG_DIRECT2D "Create SolidBrush failed");

	This->source = brush;
}

void IDirectXBrushDestory(IDirectXBrush* source) 
{
	if (source == nullptr) return;
	delete source;
}
