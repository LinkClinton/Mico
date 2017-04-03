#include "pch.hpp"

Shader::~Shader() 
{
	switch (shadertype)
	{
	case eVertexShader: {
		ID3D11VertexShader* vertexshader = (ID3D11VertexShader*)shader;
		release(vertexshader);
		break;
	}
	case ePixelShader: {
		ID3D11PixelShader* pixelshader = (ID3D11PixelShader*)shader;
		release(pixelshader);
		break;
	}
	default:
		break;
	}
	release(shaderblob);
}

void ShaderCompile(Shader* source) {
	ID3DBlob* errorblob = nullptr;

	LPCSTR target;

	switch (source->shadertype)
	{
	case ShaderType::eVertexShader:
		target = "vs_5_0";
		break;
	case ShaderType::ePixelShader:
		target = "ps_5_0";
		break;
	default:
		break;
	}

#ifdef _DEBUG
	int flag = D3DCOMPILE_DEBUG | D3DCOMPILE_SKIP_OPTIMIZATION;
#else
	int flag = D3DCOMPILE_OPTIMIZATION_LEVEL2;
#endif // _DEBUG


	HRESULT result;

	result = D3DCompileFromFile(&source->filename[0], nullptr,
		D3D_COMPILE_STANDARD_FILE_INCLUDE, &source->function[0],
		target, flag, 0, &source->shaderblob,
		&errorblob);

#ifdef _DEBUG
	if (errorblob != nullptr) {
#ifdef _CONSOLE
		std::cout << "Compile Shader Error" << std::endl;
		std::cout << (char*)errorblob->GetBufferPointer() << std::endl;
#else 
		MessageBoxA(nullptr, (char*)errorblob->GetBufferPointer(), "ErrorBox", 0);
#endif // _CONSOLE


	}
#endif // _DEBUG


	release(errorblob);
}

void ShaderCreate(Shader** source, LPCWSTR filename, LPCWSTR entrypoint,
	ShaderType type, bool IsCompile, Manager* manager)
{
	std::ifstream shaderfile;

	This = new Shader();

	This->filename = filename;
	This->shadertype = (ShaderType)type;

	int function_len = lstrlen(entrypoint);

	for (int i = 0; i < function_len; i++)
		This->function.push_back((char)entrypoint[i]);

	shaderfile.open(This->filename, std::ios::binary);

	while (shaderfile.eof() == false)
		This->shadercode.push_back((byte)shaderfile.get());

	shaderfile.close();
	This->shadercode.pop_back();

	if (IsCompile == false)
		ShaderCompile(This);

	switch (type)
	{
	case eVertexShader: {
		ID3D11VertexShader* vertexshader = nullptr;

		if (This->shaderblob != nullptr)
			result = manager->device3d->CreateVertexShader(This->shaderblob->GetBufferPointer(),
				This->shaderblob->GetBufferSize(), nullptr, &vertexshader);
		else
			result = manager->device3d->CreateVertexShader(&This->shadercode[0], This->shadercode.size(),
				nullptr, &vertexshader);

		DEBUG_RESULT(DEBUG_DIRECT3D "Create vertexshader failed");

		This->shader = vertexshader;
		break;
	}
	case ePixelShader: {
		ID3D11PixelShader* pixelshader = nullptr;

		if (This->shaderblob != nullptr)
			result = manager->device3d->CreatePixelShader(This->shaderblob->GetBufferPointer(),
				This->shaderblob->GetBufferSize(), nullptr, &pixelshader);
		else
			result = manager->device3d->CreatePixelShader(&This->shadercode[0], This->shadercode.size(),
				nullptr, &pixelshader);

		DEBUG_RESULT(DEBUG_DIRECT3D "Create pixelshader failed");

		This->shader = pixelshader;
		break;
	}
	default:
		break;
	}
}

void ShaderDestory(Shader* source)
{
	if (source == nullptr) return;
	delete source;
}