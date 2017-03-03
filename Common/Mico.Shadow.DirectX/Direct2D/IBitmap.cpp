#include"..\pch.hpp"

typedef ID2D1Bitmap This;

void IBitmapCreate(This** source, IRenderTarget* target, LPCWSTR filename)
{
	IWICBitmapDecoder *pDecoder = nullptr;
	IWICBitmapFrameDecode *pSource = nullptr;
	IWICFormatConverter *pConverter = nullptr;


	target->factory->iwic_factory->CreateDecoderFromFilename(
		filename, nullptr, GENERIC_READ,
		WICDecodeMetadataCacheOnLoad, &pDecoder);

	pDecoder->GetFrame(0, &pSource);

	target->factory->iwic_factory->CreateFormatConverter(&pConverter);

	pConverter->Initialize(pSource,
		GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone,
		NULL, 0.f, WICBitmapPaletteTypeMedianCut);

	target->target->CreateBitmapFromWicBitmap(
		pConverter, nullptr, source);

	pConverter->Release();

	pSource->Release();

	pDecoder->Release();
}

auto IBitmapGetWidth(This* source)->float
{
	return source->GetSize().width;
}

auto IBitmapGetHeight(This* source)->float
{
	return source->GetSize().height;
}

void IBitmapDestory(This* source)
{
	if (source == nullptr) return;
	source->Release();
}

