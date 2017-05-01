# Mico.DirectX

Render library.

## Class

### Direct3D

**static** library.

It is same as Direct3D11.

  - Set Render State.
    ```C#
    Direct3D.FillMode = FillMode.Solid;
    Direct3D.CullMode = CullMode.CullNone;
    //default setting
    ```
  - Set Shader.
    ```C#
    Direct3D.SetShader(Shader shader);
    //support vertex shader,pixel shader.
    ```
  - Set Buffer to Shader.
    ```C#
    Direct3D.SetBufferToXXXShader(Buffer buffer,int BufferID);
    ```
  - Set Vertex Buffer,Index Buffer.
    ```C#
    Direct3D.SetBuffer(Buffer buffer);
    //support vertex buffer,index buffer.
    ```
  - Render Object. 
    ```C#
    Direct3D.DrawXXX();
    ```

### Surface 

**RenderTarget**.

A surface to present,you must set a surface before you use Direct3D.

