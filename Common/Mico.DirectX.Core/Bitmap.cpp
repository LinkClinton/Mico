#include "pch.hpp"

Bitmap::~Bitmap()
{
	release(bitmap);
}

void BitmapCreate(Bitmap** source, LPCWSTR filename,
	float& width, float &height, Manager* manager)
{
	This = new Bitmap();

	IWICBitmapDecoder *pDecoder = nullptr;
	IWICBitmapFrameDecode *pSource = nullptr;
	IWICFormatConverter *pConverter = nullptr;

	result = manager->imagefactory->CreateDecoderFromFilename(
		filename, nullptr, GENERIC_READ,
		WICDecodeMetadataCacheOnLoad, &pDecoder);

	DEBUG_RESULT(DEBUG_WIC "Create Decoder from file failed");

	result = pDecoder->GetFrame(0, &pSource);

	DEBUG_RESULT(DEBUG_WIC "Get first Frame failed");

	result = manager->imagefactory->CreateFormatConverter(&pConverter);

	DEBUG_RESULT(DEBUG_WIC "Create FormatConverter failed");

	result = pConverter->Initialize(pSource,
		GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone,
		nullptr, 0.f, WICBitmapPaletteTypeMedianCut);

	DEBUG_RESULT(DEBUG_WIC "Initialize FormatConverter failed");

	result = manager->context2d->CreateBitmapFromWicBitmap(
		pConverter, nullptr, &This->bitmap);

	DEBUG_RESULT(DEBUG_DIRECT2D "Create Bitmap from Wic failed");

	pConverter->Release();
	pSource->Release();
	pDecoder->Release();

	width = This->bitmap->GetSize().width;
	height = This->bitmap->GetSize().height;
}

void BitmapDestory(Bitmap* source)
{
	if (source == nullptr) return;
	delete source;
}