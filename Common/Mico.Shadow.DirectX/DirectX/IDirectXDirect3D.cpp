#include "..\pch.hpp"

void IDirectXDeviceSetShader(IDirectXDevice* source, IDirectXShader* shader)
{
	switch (shader->shadertype)
	{
	case ShaderType::eVertexShader: {

		ID3D11VertexShader* vertexshader = nullptr;
		This.context3d->VSGetShader(&vertexshader, nullptr, nullptr);

		if (vertexshader != nullptr)
			vertexshader->Release(), vertexshader = nullptr;

		if (shader->shaderblob != nullptr) {
			This.device3d->CreateVertexShader(shader->shaderblob->GetBufferPointer(),
				shader->shaderblob->GetBufferSize(), nullptr, &vertexshader);
		}
		else {
			This.device3d->CreateVertexShader(&shader->shadercode[0],
				shader->shadercode.size(), nullptr, &vertexshader);
		}

		This.context3d->VSSetShader(vertexshader, nullptr, 0);
		break;
	}
	case ShaderType::ePixelShader: {

		ID3D11PixelShader* pixelshader = nullptr;
		This.context3d->PSGetShader(&pixelshader, nullptr, nullptr);

		if (pixelshader != nullptr)
			pixelshader->Release(), pixelshader = nullptr;

		if (shader->shaderblob != nullptr) {
			This.device3d->CreatePixelShader(shader->shaderblob->GetBufferPointer(),
				shader->shaderblob->GetBufferSize(), nullptr, &pixelshader);
		}
		else {
			This.device3d->CreatePixelShader(&shader->shadercode[0],
				shader->shadercode.size(), nullptr, &pixelshader);
		}

		This.context3d->PSSetShader(pixelshader, nullptr, 0);

		break;
	}
	default:
		break;
	}
}