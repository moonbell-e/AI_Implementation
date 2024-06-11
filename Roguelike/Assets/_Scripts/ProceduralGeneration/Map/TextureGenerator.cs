using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int chumkSize)
    {
        Texture2D texture = new Texture2D(chumkSize, chumkSize);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int chumkSize = heightMap.GetLength(0);

        Color[] colorMap = new Color[chumkSize * chumkSize];
        for (int y = 0; y < chumkSize; y++)
        {
            for (int x = 0; x < chumkSize; x++)
            {
                colorMap[y * chumkSize + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColorMap (colorMap, chumkSize);
    }
}
