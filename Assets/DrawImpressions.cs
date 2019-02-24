using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawImpressions : MonoBehaviour {

    public Camera _camera;
    public Shader _shader;

    [Range(1,500)]
    public float _bSize;

    [Range(0, 1)]
    public float _bStrength;

    private RenderTexture _splatmap;
    private Material _snow;
    private Material _draw;

    private RaycastHit _hit;

    // Use this for initialization
    void Start () {
        _draw = new Material(_shader);
        _draw.SetVector("_Color", Color.red);

        _snow = GetComponent<MeshRenderer>().material; // tesselation shader
        _splatmap = new RenderTexture(4096, 4096, 0, RenderTextureFormat.ARGBFloat);
        _snow.SetTexture("_Splatmap", _splatmap);

	}
	
	// Update is called once per frame
	void Update () {

        // listen for mouse press
		if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }

        // raycasting towards mesh
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit))
        {
            return;
        }

        _draw.SetVector("_Coordinates", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
        _draw.SetFloat("_Strength", _bStrength);
        _draw.SetFloat("_Size", _bSize);
        RenderTexture tmp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(_splatmap, tmp);
        Graphics.Blit(tmp, _splatmap, _draw);
        RenderTexture.ReleaseTemporary(tmp);
	}
}
