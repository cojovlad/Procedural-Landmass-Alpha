using UnityEngine;
using System.Collections;

public static class TextureGenerator {

    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) {
        //creating a 2 texture with size 2*width*height)
        Texture2D texture = new Texture2D (width, height);
        //fixes bluriness
        texture.filterMode = FilterMode.Point;
        //fixes being able to see from other side of map
        texture.wrapMode = TextureWrapMode.Clamp;
        //sets colors
        texture.SetPixels (colorMap);
        texture.Apply ();
        return texture;
    }
    public static Texture2D TextureFromHeightMap(float[,] heightMap) {
        //method returns number of elements in the row direction in a multidimensional arra
        int width = heightMap.GetLength (0);//returns rows
        int height = heightMap.GetLength (1);//returnw columns

        Color[] colorMap = new Color[width * height];
        //we iterate through h and w l in order to color the map acordingly to its height and width with a color between black and white
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colorMap [y * width + x] = Color.Lerp (Color.black, Color.white, heightMap [x, y]);
            }
        }

        return TextureFromColorMap (colorMap, width, height);
    }

}