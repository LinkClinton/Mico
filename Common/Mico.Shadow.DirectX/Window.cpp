#include"pch.hpp"


#ifndef HINST_THISCOMPONENT
EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT ((HINSTANCE)&__ImageBase)
#endif

#include<cmath>
#include<Windows.h>

#ifdef CreateWindow
#undef CreateWindow
#endif // CreateWindow


const float default_dpix = 96.0f;
const float default_dpiy = 96.0f;

HWND CreateWindow(LPCWSTR Title, LPCWSTR Ico,
	int Width, int Height, WNDPROC proc)
{
	HINSTANCE Hinstance = HINST_THISCOMPONENT;

	WNDCLASS WindowClass;

	HWND Hwnd;

	WindowClass.style = CS_HREDRAW | CS_VREDRAW;
	WindowClass.lpfnWndProc = proc;
	WindowClass.cbClsExtra = 0;
	WindowClass.cbWndExtra = 0;
	WindowClass.hInstance = Hinstance;
	WindowClass.hIcon = (HICON)LoadImageW(0, Ico, IMAGE_ICON, 0, 0, LR_LOADFROMFILE);
	WindowClass.hCursor = LoadCursor(nullptr, IDC_ARROW);
	WindowClass.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	WindowClass.lpszMenuName = NULL;
	WindowClass.lpszClassName = Title;

	RegisterClass(&WindowClass);

	FLOAT dpiX;
	FLOAT dpiY;

	ID2D1Factory* g_factory;
	HeapSetInformation(NULL, HeapEnableTerminationOnCorruption, NULL, 0);

	D2D1CreateFactory(D2D1_FACTORY_TYPE::D2D1_FACTORY_TYPE_SINGLE_THREADED,
		(ID2D1Factory**)&g_factory);

	g_factory->ReloadSystemMetrics();
	g_factory->GetDesktopDpi(&dpiX, &dpiY);

	g_factory->Release();

	RECT rc;
	rc.top = 0;
	rc.left = 0;
	rc.right = (LONG)ceil(Width*dpiX / default_dpix);
	rc.bottom = (LONG)ceil(Height*dpiY / default_dpiy);

	AdjustWindowRect(&rc, WS_OVERLAPPEDWINDOW, FALSE);


	Hwnd = CreateWindowW(Title, Title, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT,
		rc.right - rc.left,
		rc.bottom - rc.top, nullptr, nullptr, Hinstance, nullptr);


	ShowWindow(Hwnd, SW_SHOWNORMAL);

	return Hwnd;
}
