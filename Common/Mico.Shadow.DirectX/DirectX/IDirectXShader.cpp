#include "..\pch.hpp"

#include<wchar.h>


IDirectXShader::~IDirectXShader() {
	release(shaderblob);
}

void IDirectXShaderCreate(IDirectXShader** source,LPCWSTR filename,
	LPCWSTR entrypoint, int type) {
	std::ifstream shaderfile;

	This = new IDirectXShader();

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
}

void IDirectXShaderDestory(IDirectXShader* source) {
	if (source == nullptr) return;
	delete source;
}

void IDirectXShaderCompile(IDirectXShader* source) 
{
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


	D3DCompileFromFile(&source->filename[0], nullptr,
		D3D_COMPILE_STANDARD_FILE_INCLUDE, &source->function[0],
		target, flag, 0, &source->shaderblob,
		&errorblob);


	release(errorblob);
}



