#include"../pch.hpp"

IDirectXBitmap::~IDirectXBitmap() 
{
	release(source);
}

void IDirectXBitmapCreate(IDirectXBitmap** source, IDirectXDevice* device,
	LPCWSTR filename, float &width, float &height)
{
	This = new IDirectXBitmap();

	IWICBitmapDecoder *pDecoder = nullptr;
	IWICBitmapFrameDecode *pSource = nullptr;
	IWICFormatConverter *pConverter = nullptr;

	HRESULT result = device->image_factory->CreateDecoderFromFilename(
		filename, nullptr, GENERIC_READ,
		WICDecodeMetadataCacheOnLoad, &pDecoder);

	DEBUG_LOG(result, DEBUG_WIC "Create Decoder from file failed");

	result = pDecoder->GetFrame(0, &pSource);

	DEBUG_LOG(result, DEBUG_WIC "Get first Frame failed");

	result = device->image_factory->CreateFormatConverter(&pConverter);

	DEBUG_LOG(result, DEBUG_WIC "Create FormatConverter failed");

	result = pConverter->Initialize(pSource,
		GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone,
		nullptr, 0.f, WICBitmapPaletteTypeMedianCut);
	
	DEBUG_LOG(result, DEBUG_WIC "Initialize FormatConverter failed");

	result = device->context2d->CreateBitmapFromWicBitmap(
		pConverter, nullptr, &This->source);

	DEBUG_LOG(result, DEBUG_DIRECT2D "Create Bitmap from Wic failed");

	pConverter->Release();
	pSource->Release();
	pDecoder->Release();
}

void IDirectXBitmapDestory(IDirectXBitmap* source)
{
	if (source == nullptr) return;
	delete source;
}



