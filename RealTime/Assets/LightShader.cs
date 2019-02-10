using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShader : ShaderScript
{

    public Texture2D texture;
    public Light targetLight;

    public float ambientStr = 0.5f;
    public float specularStr = 0.7f;
    public float diffuseCoeff;
    public float specularCoeff;

    public Color ambient;
    public Color diffuse;
    public Color specular;

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
        Vector3 interpolatedNormal = (n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z).normalized;
         
        Vector3 normalizedlightDirection = Vector3.zero; // = (targetLight.transform.position - hitInfo.point).normalized;
        
        //targetLight.type
        //point lights also have a range
        if (targetLight.type == LightType.Point)
        {
            //position but no direction
            normalizedlightDirection = (targetLight.transform.position - hitInfo.point).normalized;
        }
        else if (targetLight.type == LightType.Directional)
        {
            //direction but no position, inverse rotation of the light
            normalizedlightDirection = -targetLight.transform.forward;
        }
        
        //normal dot light
        float nDotL = Vector3.Dot(interpolatedNormal, normalizedlightDirection);
        
        Ray shadowCheck = new Ray(hitInfo.point + (interpolatedNormal * .01f), normalizedlightDirection);

        bool shaded = Physics.Raycast(shadowCheck, Vector3.Distance(hitInfo.point, normalizedlightDirection));
        
        if (shaded)
        {
            diffuseCoeff = targetLight.intensity * Mathf.Max(nDotL, 0.0f) * 0.0f;

        }
        else
        {
            diffuseCoeff = targetLight.intensity * Mathf.Max(nDotL, 0.0f);
        }

        //specular
        Vector3 viewVec = (Camera.main.transform.position - hitInfo.point).normalized;
        Vector3 reflectVec = Vector3.Reflect(-normalizedlightDirection, interpolatedNormal);

        specularCoeff = Mathf.Pow(Mathf.Max(Vector3.Dot(viewVec, reflectVec), 0), 32);

        //calc component colors for diffuse spec and ambient
        diffuse  = Color.white * diffuseCoeff;
        ambient = Color.white * ambientStr;
        specular = Color.white * specularCoeff;


        fragColor = fragColor * (diffuse + specular + ambient);// + specular);//(diffuse + ambient);// * specular;

        fragColor.a = 1.0f;       

        return fragColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
