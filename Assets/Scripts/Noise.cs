using UnityEngine;
using System.Collections;

public static class Noise {

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapWidth,mapHeight];
		//for a new seed each time we generate
        System.Random prng = new System.Random (seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
		// we iterate the octaves in order to get different seeds and we aquire that with the offsets, we don t want components to be too high
        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next (-100000, 100000) + offset.x;
            float offsetY = prng.Next (-100000, 100000) + offset.y;
            octaveOffsets [i] = new Vector2 (offsetX, offsetY);
        }
		 //if the scale is smaller or equal to 0 then we give a the first possible value after 0
        if (scale <= 0) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
		//(1)we initialize these values because we want to be able to zoom in in the middle with "NoiseScale" button and thus we get the middle 
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

		//we iterate through the map height and width on the x and y coordinates
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
				//creating frequency and amplitude variables
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
					//we divide by the scale in order to not get the same value everytime so that's why we need the scale
                   	//we multiply by frequency in order to get better areas; the higher the frequency the higher the sample points will be which means hight values will change more rapidly
					//(1) is happening here 
					float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;
					//we multiply by two and subtract 1 in order to get a range from -1 to 1 instead of 0 to 1
                    float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
		
                    amplitude *= persistance;	//deacreases each octave bcs persistance 0-1
                    frequency *= lacunarity;	//increases each octave bcs lacunarity 1>=
                }
				//we do this in order to normalize; keep the noiseMap values in the rage 0 to 1
                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap [x, y] = noiseHeight;
            }
        }
		//we iterate once more in order to give the noise map values between min and max with "Mathf.InverseLerp"
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
            }
        }

        return noiseMap;
    }

}