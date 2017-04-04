#include "pch.hpp"

BufferLayout::~BufferLayout()
{
	release(layout);
}

void BufferLayoutCreate(BufferLayout** source, BufferLayoutElement element[], int elementsize,
	Manager* manager)
{
	This = new BufferLayout();

	std::vector<D3D11_INPUT_ELEMENT_DESC> desc(elementsize);

	int bit_off = 0;

	for (size_t i = 0; i < desc.size(); i++) {
		desc[i].SemanticName = element[i].tag;

		int add_off = 0;

		switch (element[i].size)
		{
		case BufferLayoutElementSize::eFloat1: {
			add_off = 4;
			desc[i].Format = DXGI_FORMAT_R8G8B8A8_UINT;
			break;
		}
		case BufferLayoutElementSize::eFloat2: {
			add_off = 8;
			desc[i].Format = DXGI_FORMAT_R32G32_FLOAT;
			break;
		}
		case BufferLayoutElementSize::eFloat3: {
			add_off = 12;
			desc[i].Format = DXGI_FORMAT_R32G32B32_FLOAT;
			break;
		}
		case BufferLayoutElementSize::eFloat4: {
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

	DEBUG_BOOL(manager->currentVS == nullptr, DEBUG_MANAGER "Create BufferInput failed, VertexShader is not set");

	if (manager->currentVS->shaderblob != nullptr)
		result = manager->device3d->CreateInputLayout(&desc[0],
			elementsize, manager->currentVS->shaderblob->GetBufferPointer(),
			manager->currentVS->shaderblob->GetBufferSize(), &This->layout);
	else
		result = manager->device3d->CreateInputLayout(&desc[0], elementsize,
			&manager->currentVS->shadercode[0], manager->currentVS->shadercode.size(),
			&This->layout);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create BufferInput failed");


}

void BufferLayoutDestory(BufferLayout* source)
{
	if (source == nullptr) return;
	delete source;
}