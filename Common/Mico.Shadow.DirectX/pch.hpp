#pragma once

#include<d2d1_3.h>
#include<dwrite_3.h>
#include<wincodec.h>


#pragma comment(lib,"d2d1.lib")
#pragma comment(lib,"dwrite.lib")
#pragma comment(lib,"windowscodecs.lib")


struct IFactory {
	ID2D1Factory1* d2d1_factory;
	IDWriteFactory* write_factory;
	IWICImagingFactory* iwic_factory;
	~IFactory() {
		d2d1_factory->Release();
		iwic_factory->Release();
		write_factory->Release();
	}
};