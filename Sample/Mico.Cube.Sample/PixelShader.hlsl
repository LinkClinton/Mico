
struct Input
{
    float3 pos : POSITION;
    float4 posH : SV_POSITION;
    float4 color : COLOR;
    float2 tex : TEXCOORD;
    float3 ori_pos : POSITIONT;
};


SamplerState samTex
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

Texture2D Tex : register(s0);

float4 main(Input input) : SV_TARGET
{
    return Tex.Sample(samTex, input.tex);
}