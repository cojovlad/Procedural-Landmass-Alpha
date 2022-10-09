using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator{
    //it is mesh bcs we cannot generate new meshes inside threads we have to do it outside so our game doesn t freeze
    public static MeshData GenerateTerrainMesh(float[,]heightMap)
    {
        //getting the width and the height from our 2d array
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        // we use this formula in order to be able to get the middle point x x x
        float topLeftX = (width - 1) / -2f;
        //same here but the zed value is positive
        float topLeftZ = (height - 1) / 2f;
        MeshData meshData = new MeshData(width,height);
        int vertexIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x, heightMap [x, y], topLeftZ - y);
                //we need to tell each vertices where it is in relation to the map and our values are between 0 and 1
                meshData.uvs [vertexIndex] = new Vector2 (x / (float)width, y / (float)height);
                //we do this bcs we never get to the right and down edge of the map with our parsing
                if (x < width - 1 && y < height - 1)
                {
             ]
             \
DSXXZXΩΩ`                    meshData.AddTriangle (vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                    
                vertexIndex++; 
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    //used for showing the actual scaling
    public Vector2[] uvs;
    public int[] triangles;

    private int triangleIndex;

    public MeshData(int meshWidth,int meshHeight)
    {
        //getting the number of vertices
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        
        
        //getting the number of triangles
        triangles = new int[(meshWidth-1)*(meshHeight-1)*6];    }
    //giving it 3 indices for the three vertices that will make the triangle
    public void AddTriangle(int a, int b, int c) {
        triangles [triangleIndex] = a;
        triangles [triangleIndex + 1] = b;
        triangles [triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals ();
        return mesh;
    }
}