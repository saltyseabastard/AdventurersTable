using UnityEngine;
using System.Collections;

public class CardFace : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateImage(Texture2D newImage)
    {
        var objRenderer = GetComponent<Renderer>();
        objRenderer.material.mainTexture = newImage;
    }
}
