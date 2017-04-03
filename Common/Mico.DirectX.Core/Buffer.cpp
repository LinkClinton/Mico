#include "pch.hpp"


Buffer::~Buffer()
{
	release(buffer);
}

void BufferCreate(Buffer** source, void* buffer, int size, BufferType type, Manager* manager)
{
	This = new Buffer();

	This->buffertype = type;

	D3D11_BUFFER_DESC desc = { 0 };

	desc.ByteWidth = (UINT)size;
	desc.Usage = D3D11_USAGE_DEFAULT;

	D3D11_SUBRESOURCE_DATA data = { 0 };
	data.pSysMem = buffer;
	data.SysMemPitch = 0;
	data.SysMemSlicePitch = 0;

	switch (This->buffertype)
	{
	case BufferType::eVertexBuffer:
		desc.BindFlags = D3D11_BIND_VERTEX_BUFFER;
		break;
	case BufferType::eIndexBuffer:
		desc.BindFlags = D3D11_BIND_INDEX_BUFFER;
		break;
	case BufferType::eConstBuffer:
		desc.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
		break;
	default:
		break;
	}

	result = manager->device3d->CreateBuffer(&desc, &data, &This->buffer);

	DEBUG_RESULT(DEBUG_DIRECT3D "Create Buffer failed");	
}

void BufferDestory(Buffer* source)
{
	if (source == nullptr) return;
	delete source;
}

void BufferUpdate(Buffer* source, void* data, Manager* manager)
{
	manager->context3d->UpdateSubresource(This.buffer, 0, nullptr, data, 0, 0);
}
