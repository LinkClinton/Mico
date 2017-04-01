
struct Input
{
    float3 pos : POSITION;
    float4 color : COLOR;
};

struct Output
{
    float3 pos : POSITION;
    float4 color : COLOR;
};

Output main(Input input)
{
    Output result;


    result.pos = input.pos;
    result.color = input.color;

    return result;
}