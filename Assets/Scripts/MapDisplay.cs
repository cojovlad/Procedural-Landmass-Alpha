using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {
    

    public Renderer textureRender;
    //getting a public reference
    public MeshFilter meshFilter;

    public MeshRenderer meshRenderer;
    //changed from previous DrawNoiseMap to draw texture in order to be able to render different kind of textures
    public void DrawTexture(Texture2D texture) {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
    }
    //displaying mesh
    public void DrawMesh(MeshData meshData, Texture2D texture )
    {
        //we generate outside of gamemode
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
	
}
