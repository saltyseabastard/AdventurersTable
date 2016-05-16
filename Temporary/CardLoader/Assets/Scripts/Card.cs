using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public CardFace front;
    public CardFace back;
    public CardSide left;
    public CardSide right;
    public CardSide bottom;
    public CardSide top;

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
}
