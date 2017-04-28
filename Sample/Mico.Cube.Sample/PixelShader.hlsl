
struct Input
{
    float3 pos : POSITION;
    float4 posH : SV_POSITION;
    float4 color : COLOR;
};

float4 main(Input input) : SV_TARGET
{
    float4 output = input.color;

    if (input.pos.x >= 800)
        return float4(1, 0, 0, 1);

    return output;
}