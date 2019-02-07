using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShader : ShaderScript {

    

	// Use this for initialization
	void Start () {

    }

    public override Color calcColor(RaycastHit hitInfo)
    {

        Color fragColor;

        Vector2 circlePos = new Vector2(0.5f, 0.5f);
        float radius = 0.25f;

        if ((hitInfo.textureCoord - circlePos).magnitude < radius)
        {
            fragColor = Color.white;
        }
        else
            fragColor = hitInfo.transform.GetComponent<ShaderScript>().mColor;

        return fragColor;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
