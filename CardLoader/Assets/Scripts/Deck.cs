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

        private readonly char sl; // Slash, either '/' or  '\'
        private readonly string FrontPotraitPath;
        private readonly string FrontLandscapePath;
        private readonly string BackPotraitPath;
        private readonly string BackLandscapePath;
        private readonly string SidesPath;

        private readonly DirectoryInfo DefaultSidesFolder;

        private Dictionary<string, Texture2D> FrontPortraitCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> FrontLandscapeCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> BackPortraitCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> BackLandscapeCards = new Dictionary<string, Texture2D> ();
        private Dictionary<string, Texture2D> SideColors = new Dictionary<string, Texture2D> ();

        private List<CardInfo> AllCards = new List<CardInfo> ();
        private static System.Random rng = new System.Random();

        public Deck (DirectoryInfo rootDeckDirectory)
        {
            Debug.Log ("Deck Constructor");
            if (!rootDeckDirectory.Exists)
            {
                throw new Exception (string.Format("Root path for deck {0} does not exist", rootPath.Name));
            }

            rootPath = rootDeckDirectory;
            sl = Path.DirectorySeparatorChar;
            FrontPotraitPath = @"\front\portrait";
            FrontLandscapePath = @"\front\landscape";
            BackPotraitPath = @"\back\portrait";
            BackLandscapePath = @"\back\landscape";
            SidesPath = @"\sides";

            FrontPotraitPath.Replace ('\\', sl);
            FrontLandscapePath.Replace ('\\', sl);
            BackPotraitPath.Replace ('\\', sl);
            BackLandscapePath.Replace ('\\', sl);
            SidesPath.Replace ('\\', sl);

            DefaultSidesFolder = new DirectoryInfo(rootPath.Parent.ToString() + sl + "DefaultSides");
            FindCards ();
        }

        public Texture2D GetNextCard()
        {
            Debug.Log ("Remaining cards in deck: " + Count ());
            foreach (var cardInfo in AllCards)
            {
                if (cardInfo.isInDeck == true)
                {
                    // card is in the deck, so 'draw it'
                    cardInfo.isInDeck = false;
                    Texture2D texture;
                    if (FrontPortraitCards.ContainsKey (cardInfo.path))
                    {
                        texture = FrontPortraitCards [cardInfo.path];
                    } else if (FrontLandscapeCards.ContainsKey (cardInfo.path))
                    {
                        texture = FrontLandscapeCards [cardInfo.path];
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
            return BackPortraitCards.FirstOrDefault ().Value;
        }

        public Texture2D GetCardSide(string fileName)
        {
            foreach (var color in SideColors)
            {
                var path = color.Key;
                var lastSlash = path.LastIndexOf (sl);
                var colorFile = path.Substring(lastSlash + 1, path.Length - lastSlash - 1);

                if (fileName.Equals(colorFile))
                {
                    return color.Value;
                }
            }
            Debug.Log ("File not found for sides, using default color.");
            return SideColors.FirstOrDefault().Value;
        }

        public void ShuffleCards()
        {
            var count = AllCards.Count;
            while (count > 1)
            {
                count--;
                var nextIndex = rng.Next (count + 1);
                var tmpValue = AllCards [nextIndex];
                AllCards [nextIndex] = AllCards [count];
                AllCards [count] = tmpValue;
                AllCards [count].isInDeck = true;
            }
        }

        public int Count()
        {
            return AllCards.Count (x => x.isInDeck == true);
        }

        private void FindCards()
        {
            Debug.Log ("FindCards()");
            var frontPortraitInfo = new DirectoryInfo (rootPath.FullName + FrontPotraitPath);
            var frontLandscapeInfo = new DirectoryInfo (rootPath.FullName + FrontLandscapePath);
            var backPortraitInfo = new DirectoryInfo (rootPath.FullName + BackPotraitPath);
            var backLandscapeInfo = new DirectoryInfo (rootPath.FullName + BackLandscapePath);

            LoadCards (frontPortraitInfo);
            LoadCards (frontLandscapeInfo);
            LoadCards (backPortraitInfo);
            LoadCards (backLandscapeInfo);
           
            if (!FrontPortraitCards.Any() && !FrontLandscapeCards.Any() && !BackPortraitCards.Any() && ! BackLandscapeCards.Any())
            {
                // TODO warning or something about no usable images for cards
                throw new Exception("No usable card images");
            }

            Debug.Log("Actually nothing bad happened");

            foreach (var card in FrontPortraitCards)
            {
                AllCards.Add (new CardInfo (card.Key, true));
            }
            foreach (var card in FrontLandscapeCards)
            {
                AllCards.Add (new CardInfo(card.Key, true));
            }

            ShuffleCards ();

            var sides = new DirectoryInfo (rootPath.ToString() + SidesPath);
            if (sides.Exists)
            {
                LoadCards (sides);
            }
            else
            {
                // load default sides
                LoadCards (DefaultSidesFolder);
            }
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

                        if (cardsDir.ToString ().Equals (DefaultSidesFolder.ToString()) 
                            || cardsDir.ToString().EndsWith(SidesPath))
                        {
                            SideColors.Add (image.ToString (), tmpTexture);
                        }

                        if (cardsDir.ToString().Contains (FrontPotraitPath))
                        {
                            FrontPortraitCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (FrontLandscapePath))
                        {
                            FrontLandscapeCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (BackPotraitPath))
                        {
                            BackPortraitCards.Add (image.ToString(), tmpTexture);
                        }
                        else if (cardsDir.ToString().Contains (BackLandscapePath))
                        {
                            BackLandscapeCards.Add (image.ToString(), tmpTexture);
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