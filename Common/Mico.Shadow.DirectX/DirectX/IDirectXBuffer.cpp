#include "..\pch.hpp"

IDirectXBuffer::~IDirectXBuffer()
{
	release(source);
}

void IDirectXBufferCreate(IDirectXBuffer** source, IDirectXDevice* device, void* buffer, int size, int type)
{
	This = new IDirectXBuffer();

	This->buffertype = (BufferType)type;
	This->device = device;

	D3D11_BUFFER_DESC desc;

	desc.ByteWidth = (UINT)size;
	desc.Usage = D3D11_USAGE_DEFAULT;

	D3D11_SUBRESOURCE_DATA data;
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

	device->device3d->CreateBuffer(&desc, &data, &This->source);
}

void IDirectXBufferDestory(IDirectXBuffer* source) 
{
	if (source == nullptr) return;
	delete source;
}

void IDirectXBufferUpdate(IDirectXBuffer* source, void* data) {
	source->device->context3d->UpdateSubresource(This.source, 0, nullptr,
		data, 0, 0);
}
