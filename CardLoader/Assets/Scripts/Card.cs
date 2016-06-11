using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public CardFace front;
    public CardFace back;
    public List<CardSide> sides;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateFront(Texture2D newImage)
    {
        front.UpdateImage (newImage);
    }

    public void UpdateBack(Texture2D newImage)
    {
        back.UpdateImage (newImage);
    }

    public void SetSides(Color sideColor)
    {
        foreach (var side in sides)
        {
            side.UpdateSideColor (sideColor);
        }
    }
}
