/*
	Source File: Window.hpp
	This file has function to create window and import some windows api to C#
*/

#pragma once
#include"helper.hpp"
#include<windowsx.h>

#define KEYDOWN(vk_code) ((GetAsyncKeyState(vk_code) & 0x8000) ? 1 : 0)
#define KEYUP(vk_code) ((GetAsyncKeyState(vk_code) & 0x8000) ? 0 : 1)

#ifdef CreateWindow
#undef CreateWindow
#endif

const float default_dpix = 96.f;
const float default_dpiy = 96.f;


extern int __stdcall LowWord(LPARAM lParam);

extern int __stdcall HighWord(LPARAM lParam);

extern bool __stdcall IsKeyDown(int keycode);

extern void __stdcall SetWindowSize(HWND hwnd, int width, int height);

extern HWND __stdcall CreateWindow(LPCWSTR title, LPCWSTR ico,
	int width, int height, WNDPROC proc);



