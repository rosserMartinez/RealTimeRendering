using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShader : ShaderScript
{

    public Texture2D texture;
    public Light targetLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override Color calcColor(RaycastHit hitInfo)
    {
        if (targetLight == null)
        {
            return Color.black;
        }
        
            Color fragColor;

            if (texture != null)
            {
                fragColor = texture.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
            }
            else
            {
                fragColor = Color.red;
            }

         Mesh mesh = (hitInfo.collider as MeshCollider).sharedMesh;
         Vector3[] normals = mesh.normals;
         int[] triangles = mesh.triangles;
         Vector3 n0 = normals[triangles[hitInfo.triangleIndex * 3 + 0]];
         Vector3 n1 = normals[triangles[hitInfo.triangleIndex * 3 + 1]];
         Vector3 n2 = normals[triangles[hitInfo.triangleIndex * 3 + 2]];
         Vector3 baryCenter = hitInfo.barycentricCoordinate;
         Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;

         interpolatedNormal = interpolatedNormal.normalized;
         
         float dotProduct = Vector3.Dot(interpolatedNormal, (targetLight.transform.position - hitInfo.point).normalized);
         
         fragColor = fragColor * targetLight.intensity * Mathf.Max(dotProduct, 0.0f);
         fragColor.a = 1.0f;       
          
         return fragColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
