#pragma once

#ifdef _DEBUG
#include<iostream>
#endif // _DEBUG


#include<string>
#include<vector>
#include<fstream>

#include<d2d1_3.h>
#include<d3d11_3.h>
#include<dwrite_3.h>
#include<wincodec.h>
#include<d3dcompiler.h>



#pragma comment(lib,"d2d1.lib")
#pragma comment(lib,"d3d11.lib")
#pragma comment(lib,"dwrite.lib")
#pragma comment(lib,"windowscodecs.lib")
#pragma comment(lib,"D3DCompiler.lib")

#define This (*source)

class IDirectXShader;

enum ShaderType :int {
	eVertexShader,
	ePixelShader
};

enum BufferType :int {
	eVertexBuffer,
	eIndexBuffer,
	eConstBuffer
};

enum ElementSize {
	eFLOAT1,
	eFLOAT2,
	eFLOAT3,
	eFLOAT4
};

template<typename Interface>
void release(Interface &T) {
	if (T == nullptr) return;
	T->Release();
	T = nullptr;
}

class IDirectXDevice {
public:

	//factory
	ID2D1Factory1* d2d1_factory;
	IDWriteFactory* write_factory;
	IWICImagingFactory* image_factory;

	//d2d1
	ID2D1Device* device2d;
	ID2D1DeviceContext* context2d;

	//d3d11
	ID3D11Device* device3d;
	ID3D11DeviceContext* context3d;
	ID3D11RenderTargetView* targetview;
	ID3D11DepthStencilView* depthview;

	//dxgi
	IDXGISwapChain* chain;

	//dpi and feature
	float dpiX;
	float dpiY;

	float width;
	float height;

	D3D_FEATURE_LEVEL feature;
	UINT MSAA4xQuality;

	//current_shader
	IDirectXShader* vertexshader;
	IDirectXShader* pixelshader;

	~IDirectXDevice();
};



class IDirectXBrush {
public:
	ID2D1Brush* source;

	~IDirectXBrush();
};

class IDirectXFont {
public:
	IDWriteTextFormat* source;

	~IDirectXFont();
};

class IDirectXBitmap {
public:
	ID2D1Bitmap* source;

	~IDirectXBitmap();
};

class IDirectXShader {
public:
	ShaderType shadertype;
	ID3DBlob* shaderblob;
	

	std::wstring filename;
	std::string function;

	std::vector<byte> shadercode;

	~IDirectXShader();
};

class IDirectXBuffer {
public:
	BufferType buffertype;

	ID3D11Buffer* source;

	IDirectXDevice* device;
	
	~IDirectXBuffer();
};

struct IBufferInputElement {
	LPCSTR Tag;
	ElementSize Size;
};

class IDirectXBufferInput {
public:
	ID3D11InputLayout* source;

	~IDirectXBufferInput();


};


