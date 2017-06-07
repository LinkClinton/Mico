#pragma pack_matrix( row_major )

struct PsInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
};

struct VsInput
{
    float3 position : POSITION;
    float4 color : COLOR;
};

cbuffer Transform : register(b0)
{
    matrix view;
    matrix projection;
};
cbuffer PassObject : register(b1)
{
    matrix world;
    float4 color;
};

PsInput VSmain(VsInput input)
{
    PsInput result = (PsInput) 0;

    result.position = mul(float4(input.position, 1.f), world);
    result.position = mul(result.position, view);
    result.position = mul(result.position, projection);

    result.color = input.color;

    return result;
}



float4 PSmain(PsInput input) : SV_TARGET
{
    return color;
}