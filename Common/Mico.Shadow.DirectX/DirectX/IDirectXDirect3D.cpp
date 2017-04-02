#include "..\pch.hpp"

void IDirectXDeviceSetShader(IDirectXDevice* source, IDirectXShader* shader)
{
	HRESULT result;

	switch (shader->shadertype)
	{
	case ShaderType::eVertexShader: {

		ID3D11VertexShader* vertexshader = nullptr;
		This.context3d->VSGetShader(&vertexshader, nullptr, nullptr);

		if (vertexshader != nullptr)
			vertexshader->Release(), vertexshader = nullptr;

		if (shader->shaderblob != nullptr) {
			result = This.device3d->CreateVertexShader(shader->shaderblob->GetBufferPointer(),
				shader->shaderblob->GetBufferSize(), nullptr, &vertexshader);
		}
		else {
			result = This.device3d->CreateVertexShader(&shader->shadercode[0],
				shader->shadercode.size(), nullptr, &vertexshader);
		}

		DEBUG_LOG(result, DEBUG_DIRECT3D "Create VertexShader failed");

		This.context3d->VSSetShader(vertexshader, nullptr, 0);

		This.vertexshader = shader;
		break;
	}
	case ShaderType::ePixelShader: {

		ID3D11PixelShader* pixelshader = nullptr;
		This.context3d->PSGetShader(&pixelshader, nullptr, nullptr);

		if (pixelshader != nullptr)
			pixelshader->Release(), pixelshader = nullptr;

		if (shader->shaderblob != nullptr) {
			result = This.device3d->CreatePixelShader(shader->shaderblob->GetBufferPointer(),
				shader->shaderblob->GetBufferSize(), nullptr, &pixelshader);
		}
		else {
			result = This.device3d->CreatePixelShader(&shader->shadercode[0],
				shader->shadercode.size(), nullptr, &pixelshader);
		}

		DEBUG_LOG(result, DEBUG_DIRECT3D "Create PixelShader failed");

		This.context3d->PSSetShader(pixelshader, nullptr, 0);

		This.pixelshader = shader;
		break;
	}
	default:
		break;
	}
}

void IDirectXDeviceSetBufferInput(IDirectXDevice* source, IDirectXBufferInput* bufferinput) 
{
	This.context3d->IASetInputLayout(bufferinput->source);
}

void IDirectXDeviceSetIndexBuffer(IDirectXDevice* source, IDirectXBuffer* buffer)
{
	This.context3d->IASetIndexBuffer(buffer->source, DXGI_FORMAT_R32_UINT, 0);
}

void IDirectXDeviceSetVertexBuffer(IDirectXDevice* source, IDirectXBuffer* buffer, int eachsize)
{
	UINT stride = eachsize;
	UINT offset = 0;
	This.context3d->IASetVertexBuffers(0, 1, &buffer->source, &stride, &offset);
}

void IDirectXDeviceRenderBuffer(IDirectXDevice* source, int vertexcount, int startlocation, int PrimitiveType)
{
	This.context3d->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY(PrimitiveType));

	This.context3d->Draw(vertexcount, startlocation);
}

void IDirectXDeviceRenderBufferIndex(IDirectXDevice* source, int indexcount, int startlocation, int vertexlocation,
	int PrimitiveType)
{
	This.context3d->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY(PrimitiveType));

	This.context3d->DrawIndexed(indexcount, startlocation, vertexlocation);
}