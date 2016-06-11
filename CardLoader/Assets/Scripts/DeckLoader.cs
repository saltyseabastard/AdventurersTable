using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using DeckLoading;
using System.Linq;

public class DeckLoader : MonoBehaviour
{
    public GameObject roundedCardPrefab = null;

    private GameObject cardObj = null;
    private GameObject deckObj = null;
    private bool deckCreated;

    private DirectoryInfo decksPath;
    private const string decksDir = @"\Decks";
    private List<Deck> decks = new List<Deck>();

    private Deck activeDeck;

    private Color defaultCardSideColor = Color.black;

	// Use this for initialization
	void Start ()
    {
        decksPath = new DirectoryInfo(Directory.GetCurrentDirectory () + decksDir);
        if (!decksPath.Exists)
        {
            throw new Exception(string.Format("Directory {0} does not exist. Unable to load decks.", decksPath.ToString()));
        }

        foreach (var deckPath in decksPath.GetDirectories())
        {
            try
            {
                var tmpDeck = new Deck (deckPath);
                decks.Add(tmpDeck);
            }
            catch (Exception ex)
            {
                Debug.Log (string.Format("Exception loading deck: {0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        activeDeck = decks.First ();
	}
	
	// Update is called once per frame
    void Update ()
    {
        // TODO: Create objects if needed, then draw a card.
        if (Input.GetButtonDown("Jump"))
        {
            if (!deckCreated)
            {
                Debug.Log ("Creating deck");
                CreateDeck ();
            }
            else
            {
                DrawNextCard ();
            }
                
        }
//        if (deck == null)
//        {
//            Instantiate (deck);
//        }

//        card.
	}

    void CreateDeck()
    {
        cardObj = (GameObject)Instantiate (roundedCardPrefab, new Vector3 (0, 2, 0), Quaternion.identity);
        //created.SetActive (false);
        // rotate x -90 to face camera
        cardObj.transform.Rotate(-90, 0, 0);
        ApplyOtherTextures ();
        DrawNextCard ();
        deckCreated = true;
    }

    void DrawNextCard()
    {
        var card = cardObj.GetComponent<Card> ();
        card.UpdateFront (activeDeck.GetNextCard ());
    }

    void ApplyOtherTextures()
    {
        var card = cardObj.GetComponent<Card> ();
        card.UpdateBack(activeDeck.GetCardBack());
        card.SetSides (defaultCardSideColor);
    }
}
