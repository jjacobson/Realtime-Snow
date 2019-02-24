using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour {

    public Shader _SnowAccumulationShader;

    [Range(0.001f, 0.1f)]
    public float _SnowflakeCount;

    [Range(0f, 1f)]
    public float _SnowflakeOpacity;

    private Material _SnowMaterial;
    private MeshRenderer _Renderer;
    
	// Use this for initialization
	void Start () {
        _Renderer = GetComponent<MeshRenderer>();
        _SnowMaterial = new Material(_SnowAccumulationShader);
	}
	
	// Update is called once per frame
	void Update () {
        _SnowMaterial.SetFloat("_SnowflakeCount", _SnowflakeCount);
        _SnowMaterial.SetFloat("_SnowflakeOpacity", _SnowflakeOpacity);

        RenderTexture snow = (RenderTexture)_Renderer.material.GetTexture("_Splatmap");
        RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(snow, temp, _SnowMaterial);
        Graphics.Blit(temp, snow);
        _Renderer.material.SetTexture("_Splatmap", snow);
        RenderTexture.ReleaseTemporary(temp);
	}
}
