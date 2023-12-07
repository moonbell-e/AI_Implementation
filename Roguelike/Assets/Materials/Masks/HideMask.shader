Shader "Custom/HideMask"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" "RenderingPipeline" = "UniversalPipeline"}
    
        Pass
        {
            Blend Zero One
            ZWrite Off
        }
    }
}