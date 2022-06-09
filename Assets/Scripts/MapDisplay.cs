using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    //changed from previous DrawNoiseMap to draw texture in order to be able to render different kind of textures
    public void DrawTexture(Texture2D texture) {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
    }
	
}
