
struct Input
{
    float3 pos : POSITION;
    float4 posH : SV_POSITION;
    float4 color : COLOR;
};

float4 main(Input input) : SV_TARGET
{
    float4 output = input.color;
    return output;
}