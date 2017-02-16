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


extern int LowWord(LPARAM lParam);

extern int HighWord(LPARAM lParam);

extern bool IsKeyDown(int keycode);

extern void SetWindowSize(HWND hwnd, int width, int height);

extern HWND CreateWindow(LPCWSTR title, LPCWSTR ico,
	int width, int height, WNDPROC proc);



