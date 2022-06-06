using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale) {
		float[,] noiseMap = new float[mapWidth,mapHeight];
        //if the scale is smaller or equal to 0 then we give a the first possible value after 0

		if (scale <= 0) {
			scale = 0.0001f;
		}
        //we iterate through the map height and width on the x and y coordinates
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
               //we divide by the scale in order to not get the same value everytime so that's why we need the scale

				float sampleX = x / scale;
				float sampleY = y / scale;

				float perlinValue = Mathf.PerlinNoise (sampleX, sampleY);
				noiseMap [x, y] = perlinValue;
			}
		}

		return noiseMap;
	}

}