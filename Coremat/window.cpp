#include"window.hpp"

#include<cmath>

#ifndef HINST_THISCOMPONENT
EXTERN_C IMAGE_DOS_HEADER __ImageBase;
#define HINST_THISCOMPONENT ((HINSTANCE)&__ImageBase)
#endif


extern int __stdcall LowWord(LPARAM lParam)
{
	return LOWORD(lParam);
}

extern int __stdcall  HighWord(LPARAM lParam) 
{
	return HIWORD(lParam);
}

extern bool __stdcall IsKeyDown(int keycode) 
{
	if (keycode == 20 ||
		keycode == 144 ||
		keycode == 145) {
		short state = ::GetKeyState((int)keycode);
		if (state == 1) return true;
		return false;
	}
	return KEYDOWN((int)keycode);
}

extern void __stdcall SetWindowSize(HWND hwnd, int width, int height)
{
	SetWindowPos(hwnd, HWND_TOP, 0, 0, width, height, SWP_NOMOVE);
}

extern HWND __stdcall CreateWindow(LPCWSTR title, LPCWSTR ico,
	int width, int height, WNDPROC proc)
{
	HINSTANCE Hinstance = HINST_THISCOMPONENT;

	WNDCLASS WindowClass;

	HWND Hwnd;

	WindowClass.style = CS_HREDRAW | CS_VREDRAW;
	WindowClass.lpfnWndProc = proc;
	WindowClass.cbClsExtra = 0;
	WindowClass.cbWndExtra = 0;
	WindowClass.hInstance = Hinstance;
	WindowClass.hIcon = (HICON)LoadImageW(0, ico, IMAGE_ICON, 0, 0, LR_LOADFROMFILE);
	WindowClass.hCursor = LoadCursor(nullptr, IDC_ARROW);
	WindowClass.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	WindowClass.lpszMenuName = NULL;
	WindowClass.lpszClassName = title;

	RegisterClass(&WindowClass);

	FLOAT dpiX;
	FLOAT dpiY;

	ID2D1Factory* g_factory;
	HeapSetInformation(NULL, HeapEnableTerminationOnCorruption, NULL, 0);

	D2D1CreateFactory(D2D1_FACTORY_TYPE::D2D1_FACTORY_TYPE_SINGLE_THREADED,
		(ID2D1Factory**)&g_factory);

	g_factory->ReloadSystemMetrics();
	g_factory->GetDesktopDpi(&dpiX, &dpiY);

	release(&g_factory);

	RECT rc;
	rc.top = 0;
	rc.left = 0;
	rc.right = (LONG)ceil(width*dpiX / default_dpix);
	rc.bottom = (LONG)ceil(height*dpiY / default_dpiy);

	AdjustWindowRect(&rc, WS_OVERLAPPEDWINDOW, FALSE);


	Hwnd = CreateWindowW(title, title, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT,
		rc.right - rc.left,
		rc.bottom - rc.top, nullptr, nullptr, Hinstance, nullptr);


	ShowWindow(Hwnd, SW_SHOWNORMAL);

	return Hwnd;
}
