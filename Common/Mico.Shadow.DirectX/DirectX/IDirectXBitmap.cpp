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

	pDecoder->GetFrame(0, &pSource);

	device->image_factory->CreateFormatConverter(&pConverter);

	pConverter->Initialize(pSource,
		GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone,
		nullptr, 0.f, WICBitmapPaletteTypeMedianCut);



	device->context2d->CreateBitmapFromWicBitmap(
		pConverter, nullptr, &This->source);

	pConverter->Release();
	pSource->Release();
	pDecoder->Release();
}

void IDirectXBitmapDestory(IDirectXBitmap* source)
{
	if (source == nullptr) return;
	delete source;
}



