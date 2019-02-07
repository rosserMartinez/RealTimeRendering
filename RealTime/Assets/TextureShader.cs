using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureShader : ShaderScript {

	// Use this for initialization
	void Start () {
		
	}

    public override Color calcColor(RaycastHit hitInfo)
    {

        Color fragColor = Color.black;

        Material currentMat = hitInfo.transform.GetComponent<Renderer>().sharedMaterial;

        if (currentMat.mainTexture == true)
        {
            fragColor = (currentMat.mainTexture as Texture2D).GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        }

        return fragColor;

    }

        // Update is called once per frame
    void Update () {
		
	}
}
