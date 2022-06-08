// using System.Collections;
// using UnityEngine;
//
// public static class Noise
// {
//     public static float[,]
//     GenerateNoiseMap(
//         int mapWidth,
//         int mapHeight,
//         float scale,
//         int octaves,
//         float persistance,
//         float lacunarity
//     )
//     {
//         float[,] noiseMap = new float[mapWidth, mapHeight];
//
//         //if the scale is smaller or equal to 0 then we give a the first possible value after 0
//         if (scale <= 0)
//         {
//             scale = 0.0001f;
//         }
//         float maxNoiseHeight = float.minValue;
//         float minNoiseHeight = float.maxValue;
//
//         //we iterate through the map height and width on the x and y coordinates
//         for (int y = 0; y < mapHeight; y++)
//         {
//             for (int x = 0; x < mapWidth; x++)
//             {
//                 //creating frequency and amplitude variables 
//                 float frequency=1;
//                 float amplitude=1;
//                 
//                 float noiseHeight=0;
//                 // we are going through all ov our octaves
//                 for (int o = 0; o < octaves; o++)
//                 {
//                     //we divide by the scale in order to not get the same value everytime so that's why we need the scale
//                     //we multiply by frequency in order to get better areas; the higher the frequency the higher the sample points will be which means hight values will change more rapidly 
//                     float sampleX = x / scale * frequency;
//                     float sampleY = y / scale * frequency;
//
//                     //we multiply by two and subtract 1 in order to get a range from -1 to 1 instead of 0 to 1
//                     float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
//
//                     //we increase the noise heigh for each octave by the perlin value * amplitude
//                     noiseHeight+= perlinValue * amplitude;
//
//                     amplitude*= persistance;//deacreases each octave bcs pers 0-1
//                     frequency*= lacunarity; //increases each octave bcs lacunarity 1>=
//                 }
//                 //we do this in order to normalize; keep the noiseMap values in the rage 0 to 1
//                     if (noiseHeight > maxNoiseHeight) {
//                         maxNoiseHeight = noiseHeight;
//                     } else if (noiseHeight < minNoiseHeight) {
//                         minNoiseHeight = noiseHeight;
//                     }
//                 noiseMap [x, y] = noiseHeight;
//             }
//         }
//         for (int y = 0; y < mapHeight; y++)
//         {
//             for (int x = 0; x < mapWidth; x++)
//             {
//                 noiseMap[x,y]=Mathf.InverseLerp(minNoiseHeight,maxNoiseHeight,noiseMap[x,y]);
//             }
//         }
//         return noiseMap;
//     }
// }
sing UnityEngine;
using System.Collections;

public static class Noise {

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapWidth,mapHeight];

        System.Random prng = new System.Random (seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next (-100000, 100000) + offset.x;
            float offsetY = prng.Next (-100000, 100000) + offset.y;
            octaveOffsets [i] = new Vector2 (offsetX, offsetY);
        }

        if (scale <= 0) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;


        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
		
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap [x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
            }
        }

        return noiseMap;
    }

}