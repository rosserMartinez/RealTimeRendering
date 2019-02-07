using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour {


    public Color mColor = Color.magenta;

    virtual public Color calcColor(RaycastHit hitInfo)
    {
        return mColor; 
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
