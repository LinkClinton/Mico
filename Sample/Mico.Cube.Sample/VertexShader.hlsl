#pragma pack_matrix( row_major )


struct Input
{
    float3 pos : POSITION;
    float4 color : COLOR;
};

struct Output
{
    float3 pos : POSITION;
    float4 posH : SV_POSITION;
    float4 color : COLOR;
};

struct Transform
{
    matrix world;
    matrix view;
    matrix projection;
};

Transform transform : register(b0);

Output main(Input input)
{
    Output result;
    
    result.pos = mul(float4(input.pos, 1.f), transform.world).xyz;
    
    result.posH = mul(float4(input.pos, 1.f), (transform.world));
    result.posH = mul(result.posH, (transform.view));
    result.posH = mul(result.posH, (transform.projection));
  
    result.color = input.color;

    return result;
}