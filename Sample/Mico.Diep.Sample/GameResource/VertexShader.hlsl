struct Input
{
    float3 pos : POSITION;
    float2 tex : TEXCOORD;
    float4 color : COLOR;
};
struct Output
{
    float4 posH : SV_POSITION;
    float3 pos : POSITION;
    float2 tex : TEXCOORD;
    float4 color : COLOR;
};


Output main(Input input)
{
    Output result;
    
    result.posH = float4(0, 0, 0, 0);
    result.pos = input.pos;
    result.color = input.color;
    result.tex = input.tex;

    return result;
}




