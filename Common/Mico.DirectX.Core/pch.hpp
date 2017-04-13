#pragma once

#include<string>
#include<vector>
#include<fstream>

#include<d2d1_3.h>
#include<d3d11_3.h>
#include<dwrite_3.h>
#include<wincodec.h>
#include<d3dcompiler.h>

#define DEBUG_WIC "WIC: "
#define DEBUG_DXGI "DXGI: "
#define DEBUG_DIRECT3D "Direct3D: "
#define DEBUG_DIRECT2D "Direct2D: "
#define DEBUG_SURFACE "Surface: "
#define DEBUG_MANAGER "Manager: "

#ifdef _DEBUG
#include<iostream>
#include<assert.h>

#ifdef _CONSOLE
#define DEBUG_RESULT(expression) if (FAILED(result))  {std::cout << expression << std::endl;  throw result; }
#define DEBUG_BOOL(judge,expression) if (judge) { std::cout << expression << std::endl; throw judge; }
#else 
#define DEBUG_RESULT(expression) if (FAILED(result)) { MessageBox(nullptr, TEXT(expression), L"ErrorBox", 0); throw result;}
#define DEBUG_BOOL(judge,expression) if (judge) { MessageBox(nullptr, TEXT(expression), L"ErrorBox", 0); throw judge;}

#endif // _CONSOLE

#else
#define DEBUG_RESULT(expression) 
#define DEBUG_BOOL(judge,expression)

#endif // _DEBUG

#define This (*source)

static HRESULT result;

#pragma comment(lib,"d2d1.lib")
#pragma comment(lib,"d3d11.lib")
#pragma comment(lib,"dwrite.lib")
#pragma comment(lib,"windowscodecs.lib")
#pragma comment(lib,"D3DCompiler.lib")

template<typename Interface>
void release(Interface &T) {
	if (T == nullptr) return;
	T->Release();
	T = nullptr;
}

enum ShaderType :int {
	eVertexShader,
	ePixelShader
};

enum BufferType :int {
	eVertexBuffer,
	eIndexBuffer,
	eConstBuffer
};

enum BufferLayoutElementSize :int {
	eFloat1,
	eFloat2,
	eFloat3,
	eFloat4
};

enum PrimitiveType :int {
	UNK,
	Point,
	Line,
	LineStrip,
	Triangle,
	TriangleStrip,
};

struct BufferLayoutElement {
	BufferLayoutElementSize size;
	LPCSTR tag;
};

class Shader {
public:
	ShaderType shadertype;
	ID3DBlob* shaderblob;

	void* shader;

	std::wstring filename;
	std::string function;

	std::vector<byte> shadercode;

	~Shader();
};

class Buffer {
public:
	BufferType buffertype;

	ID3D11Buffer* buffer;

	~Buffer();
};

class BufferLayout {
public:
	ID3D11InputLayout* layout;

	~BufferLayout();
};

class Manager {
public:
	ID2D1Device* device2d;
	ID3D11Device* device3d;
	ID2D1DeviceContext* context2d;
	ID3D11DeviceContext* context3d;

	ID2D1Factory1* d2d1factory;
	IDWriteFactory* writefactory;
	IWICImagingFactory* imagefactory;



	ID3D11RenderTargetView* currentRTV;
	ID3D11DepthStencilView* currentDSV;
	Shader*					currentVS;
	Shader*					currentPS;

	IDXGISwapChain*			currentChain;
	


	D3D_FEATURE_LEVEL	featureLevel;
	UINT				msaa4xQuality;
	FLOAT				dpiX;
	FLOAT				dpiY;

	~Manager();
};

class Surface {
public:
	IDXGISwapChain* swapchain;

	ID3D11RenderTargetView* surfaceRTV;
	ID3D11DepthStencilView* surfaceDSV;

	HWND surfaceHWND;

	~Surface();
};

class Brush {
public:
	ID2D1Brush* brush;

	~Brush();
};

class Fontface {
public:
	IDWriteTextFormat* fontface;

	~Fontface();
};

class Bitmap {
public:
	ID2D1Bitmap* bitmap;

	~Bitmap();
};

