#pragma once

#include"pch.hpp"

#include<d2d1_3.h>

using namespace System;
using namespace System::Runtime::InteropServices;


namespace Mico {

	namespace Shadow {

		namespace DirectX {
			
			public ref class Direct2D {
			public:


				/*IFactory*/
				ref class IFactory {
				public:
					static auto Create()->IntPtr;
					static void Destory(IntPtr source);
					static auto GetDesktopDpi(IntPtr source)->Mico::Math::Vector2^;
					static auto GetDevice(IntPtr source, IntPtr dxgidevice)->IntPtr;
					static auto GetRenderTarget(IntPtr source, IntPtr hwnd)->IntPtr;
				};

				/*IDevice*/
				ref class IDevice {
				public:
					static auto Create(IntPtr factory, IntPtr dxgidevice)->IntPtr;
					static void Destory(IntPtr source);
					static auto GetDeviceContext(IntPtr source)->IntPtr;
				};

				/*IDeviceContext*/
				ref class IDeviceContext {
				public:
					static auto Create(IntPtr device)->IntPtr;
					static void Destory(IntPtr source);
					static void SetTarget(IntPtr source, IntPtr bitmap);
					static auto GetBitmapFromDXGISurface(IntPtr source, IntPtr dxgisurface)->IntPtr;
				};

				/*IRenderTarget*/
				ref class IRenderTarget {
				public:
					static auto Create(IntPtr factory, IntPtr hwnd)->IntPtr;
					static void Destory(IntPtr source);
				};


			};


		}

	}

}