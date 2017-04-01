#include "..\pch.hpp"

IDirectXBufferInput::~IDirectXBufferInput() 
{
	release(source);
}

void IDirectXBufferInputCreate(IDirectXBufferInput** source,
	IDirectXDevice* device, IBufferInputElement element[], int elementsize)
{
	HRESULT result;

	This = new IDirectXBufferInput();

	std::vector<D3D11_INPUT_ELEMENT_DESC> desc(elementsize);

	int bit_off = 0;

	for (size_t i = 0; i < desc.size(); i++) {
		desc[i].SemanticName = element[i].Tag;
		
		int add_off = 0;

		switch (element[i].Size)
		{
		case ElementSize::eFLOAT1: {
			add_off = 4;
			desc[i].Format = DXGI_FORMAT_R8G8B8A8_UINT;
			break;
		}
		case ElementSize::eFLOAT2: {
			add_off = 8;
			desc[i].Format = DXGI_FORMAT_R32G32_FLOAT;
			break;
		}
		case ElementSize::eFLOAT3: {
			add_off = 12;
			desc[i].Format = DXGI_FORMAT_R32G32B32_FLOAT;
			break;
		}
		case ElementSize::eFLOAT4: {
			add_off = 16;
			desc[i].Format = DXGI_FORMAT_R32G32B32A32_FLOAT;
			break;
		}
		default:
			break;
		}

		desc[i].InputSlotClass = D3D11_INPUT_PER_VERTEX_DATA;

		desc[i].AlignedByteOffset = bit_off;
		bit_off += add_off;
	}

	if (device->vertexshader->shaderblob != nullptr) {
		result = device->device3d->CreateInputLayout(&desc[0], elementsize,
			device->vertexshader->shaderblob->GetBufferPointer(),
			device->vertexshader->shaderblob->GetBufferSize(), &This->source);
	}
	else {
		result = device->device3d->CreateInputLayout(&desc[0], elementsize,
			&device->vertexshader->shadercode[0],
			device->vertexshader->shadercode.size(), &This->source);
	}

#ifdef _DEBUG
	if (FAILED(result)) {
		std::cout << "Create BufferInput Failed" << std::endl;
	}
#endif // _DEBUG

}

void IDirectXBufferInputDestory(IDirectXBufferInput* source) 
{
	if (source = nullptr) return;
	delete source;
}