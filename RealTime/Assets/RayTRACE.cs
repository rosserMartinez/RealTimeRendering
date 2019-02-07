using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTRACE : MonoBehaviour {

    public Texture2D tex;
    public int width = 512;
    public int height = 512;

    private void Awake()
    {

        tex = new Texture2D(width, height);
    }

    // Use this for initialization
    void Start () {

        //iterate through pixels
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                Ray currentPixelRay = Camera.main.ViewportPointToRay(
                    new Vector3((float)i/width, 
                    (float)j/height, 
                    Camera.main.nearClipPlane));

                RaycastHit hitInfo;
                bool hit = Physics.Raycast(currentPixelRay, out hitInfo);

                if (hit)
                {
                    ShaderScript shader = hitInfo.transform.GetComponent<ShaderScript>();

                    if (shader != null)
                    {
                        tex.SetPixel(i, j, shader.calcColor(hitInfo));
                    }
                    else
                    {
                        tex.SetPixel(i, j, Color.cyan);
                    }
                }

            }
        }



        tex.Apply();

	}
	
	// Update is called once per frame
	void OnGUI () {

        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), tex);

	}

    
}
