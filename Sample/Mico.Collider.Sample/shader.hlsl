#pragma pack_matrix( row_major )

const static float MaxDep = 2000;

struct PsInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    float dep : Depth;
};

struct VsInput
{
    float3 position : POSITION;
    float4 color : COLOR;
};

struct PsOut
{
    float4 color : SV_Target;
    float dep : SV_Depth;
};

cbuffer Transform : register(b0)
{
    matrix view;
    matrix projection;
    float4 eyePos;
};
cbuffer PassObject : register(b1)
{
    matrix world;
    float4 color;
};

PsInput VSmain(VsInput input)
{
    PsInput result = (PsInput) 0;

    float3 realPosition = mul(float4(input.position, 1), world).xyz;

    result.dep = distance(realPosition, eyePos.xyz) / MaxDep;

    result.position = mul(float4(input.position, 1.f), world);
    result.position = mul(result.position, view);
    result.position = mul(result.position, projection);


    result.color = input.color;
    
    return result;
}



PsOut PSmain(PsInput input) 
{
    PsOut result;
    result.dep = input.dep;
    result.color = color;

    return result;
}