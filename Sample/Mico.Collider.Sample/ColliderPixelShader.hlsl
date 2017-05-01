
struct Input
{
    float3 pos : POSITION;
    float4 posH : SV_POSITION;
    float4 color : COLOR;
    float3 ori_pos : POSITIONT;
};

float4 Color : register(b0);

float4 main(Input input) : SV_TARGET
{
    float4 output = input.color;

    return Color;
}