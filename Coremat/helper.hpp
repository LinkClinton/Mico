/*
	Source File:helper.hpp
	This file has some function to manager resource and other.
*/
#pragma once

#include<d3d11.h>
#include<d2d1_2.h>
#include<Windows.h>
#include<dwrite_2.h>

template<class Interface>
inline void
release(
	Interface **ppInterfaceToRelease
)
{
	if (*ppInterfaceToRelease != NULL)
	{
		(*ppInterfaceToRelease)->Release();

		(*ppInterfaceToRelease) = NULL;
	}
}