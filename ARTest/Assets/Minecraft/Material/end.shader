Shader "Custom/ScreenSpaceColor"  
{  
    Properties  
    {  
        _Color ("Color", Color) = (1,1,1,1)  
    }  
    SubShader  
    {  
        Tags { "RenderType"="Opaque" }  
        LOD 100  
  
        Pass  
        {  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
  
            #include "UnityCG.cginc"  
  
            struct appdata  
            {  
                float4 vertex : POSITION;  
                float2 uv : TEXCOORD0;  
            };  
  
            struct v2f  
            {  
                float2 uv : TEXCOORD0;  
                float4 vertex : SV_POSITION;  
            };  
  
            fixed4 _Color;  
  
            v2f vert (appdata v)  
            {  
                v2f o;  
                o.vertex = UnityObjectToClipPos(v.vertex);  
                o.uv = v.uv; // 假设输入UV已经是屏幕空间坐标（实际情况下不是）  
                return o;  
            }  
  
            fixed4 frag (v2f i) : SV_Target  
            {  
                // 这里不是真正的屏幕空间映射，但我们可以基于UV坐标来变化颜色  
                // 例如，让颜色从左上角到右下角渐变  
                fixed t = i.uv.x * i.uv.y; // 简单的变化函数，可以根据需要调整  
                fixed4 col = _Color * t;  
                return col;  
            }  
            ENDCG  
        }  
    }  
}