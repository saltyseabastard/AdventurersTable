using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DeckLoading
{
    public class Deck
    {
        class CardInfo
        {
            public string path;
            public bool isInDeck;
            public CardInfo (string cardPath, bool cardIsInDeck)
            {
                path = cardPath;
                isInDeck = cardIsInDeck;
            }
        }

        private DirectoryInfo rootPath;

        private const string frontPotraitPath = @"\front\portrait";
        private const string frontLandscapePath = @"\front\landscape";
        private const string backPotraitPath = @"\back\portrait";
        private const string backLandscapePath = @"\back\landscape";

        private Dictionary<string, Texture2D> frontPortraitCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> frontLandscapeCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> backPortraitCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> backLandscapeCards = new Dictionary<string, Texture2D> ();

        private List<CardInfo> allCards = new List<CardInfo> ();
        private static System.Random rng = new System.Random();

        public Deck (DirectoryInfo rootDeckDirectory)
        {
            Debug.Log ("Deck Constructor");
            if (!rootDeckDirectory.Exists)
            {
                throw new Exception (string.Format("Root path for deck {0} does not exist", rootPath.Name));
            }

            rootPath = rootDeckDirectory;
            FindCards ();
        }

        public Texture2D GetNextCard()
        {
            Debug.Log ("Remaining cards in deck: " + Count ());
            foreach (var cardInfo in allCards)
            {
                if (cardInfo.isInDeck == true)
                {
                    // card is in the deck, so 'draw it'
                    cardInfo.isInDeck = false;
                    Texture2D texture;
                    if (frontPortraitCards.ContainsKey (cardInfo.path))
                    {
                        texture = frontPortraitCards [cardInfo.path];
                    } else if (frontLandscapeCards.ContainsKey (cardInfo.path))
                    {
                        texture = frontLandscapeCards [cardInfo.path];
                    }
                    else
                    {
                        Debug.Log ("Something bad happened here.");
                        return null;
                    }
                    // use the first card found
                    return texture;
                }
            }

            Debug.Log ("All cards have been drawn, shuffling the deck.");
            ShuffleCards ();
            return GetNextCard ();
        }

        public Texture2D GetCardBack()
        {
            return backPortraitCards.FirstOrDefault ().Value;
        }

        public void ShuffleCards()
        {
            var count = allCards.Count;
            while (count > 1)
            {
                count--;
                var nextIndex = rng.Next (count + 1);
                var tmpValue = allCards [nextIndex];
                allCards [nextIndex] = allCards [count];
                allCards [count] = tmpValue;
                allCards [count].isInDeck = true;
            }
        }

        public int Count()
        {
            return allCards.Count (x => x.isInDeck == true);
        }

        private void FindCards()
        {
            Debug.Log ("FindCards()");
            var frontPortraitInfo = new DirectoryInfo (rootPath.FullName + frontPotraitPath);
            var frontLandscapeInfo = new DirectoryInfo (rootPath.FullName + frontLandscapePath);
            var backPortraitInfo = new DirectoryInfo (rootPath.FullName + backPotraitPath);
            var backLandscapeInfo = new DirectoryInfo (rootPath.FullName + backLandscapePath);

            LoadCards (frontPortraitInfo);
            LoadCards (frontLandscapeInfo);
            LoadCards (backPortraitInfo);
            LoadCards (backLandscapeInfo);
           
            if (!frontPortraitCards.Any() && !frontLandscapeCards.Any() && !backPortraitCards.Any() && ! backLandscapeCards.Any())
            {
                // TODO warning or something about no usable images for cards
                throw new Exception("No usable card images");
            }

            Debug.Log("Actually nothing bad happened");

            foreach (var card in frontPortraitCards)
            {
                allCards.Add (new CardInfo (card.Key, true));
            }
            foreach (var card in frontLandscapeCards)
            {
                allCards.Add (new CardInfo(card.Key, true));
            }

            ShuffleCards ();
        }

        private void LoadCards(DirectoryInfo cardsDir)
        {
            Debug.Log (string.Format("LoadCards() called for directory {0}", cardsDir.ToString()));

            if (!cardsDir.Exists)
            {
                Debug.Log("Cards directory does not exist");
                return;
            }

            var cards = cardsDir.GetFiles ();

            foreach (var image in cards)
            {
                // make sure format is either .png or .jpg
                if (!image.Name.EndsWith (".png") && !image.Name.EndsWith (".jpg"))
                {
                    // TODO warning here that file won't be loaded
                    Debug.Log("file format is invalid");
                }
                else
                {
                    // Load the file bytes
                    if (image.Exists)
                    {
                        var bytes = File.ReadAllBytes (image.ToString());
                        var tmpTexture = new Texture2D (1, 1);
                        tmpTexture.LoadImage (bytes);

                        if (cardsDir.ToString().Contains (frontPotraitPath))
                        {
                            frontPortraitCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (frontLandscapePath))
                        {
                            frontLandscapeCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (backPotraitPath))
                        {
                            backPortraitCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (backLandscapePath))
                        {
                            backLandscapeCards.Add (image.ToString(), tmpTexture);
                        }
                        else
                        {
                           // TODO error here for unknown location
                        }
                    }
                }
            }
        }
    }
}