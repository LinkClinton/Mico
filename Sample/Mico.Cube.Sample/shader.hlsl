#pragma pack_matrix( row_major )

struct PsInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    float2 tex : TEXCOORD;
};

struct VsInput
{
    float3 position : POSITION;
    float4 color : COLOR;
    float2 tex : TEXCOORD;
};

struct Transform
{
    matrix world;
    matrix view;
    matrix projection;
};

Transform transform : register(b0);

PsInput VSmain(VsInput input)
{
    PsInput result;

    result.position = mul(float4(input.position, 1.f), transform.world);
    result.position = mul(result.position, transform.view);
    result.position = mul(result.position, transform.projection);

    result.tex = input.tex;
    result.color = input.color;

    return result;
}

float4 PSmain(PsInput input) : SV_TARGET
{
    return input.color;
}