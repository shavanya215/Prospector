using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bartok : MonoBehaviour {

	// Use this for initialization
	
    static public Bartok S;
    public TextAsset deckXML;
    public Vector3 layoutCenter = Vector3.zero;

    public bool _______________;
    public Deck deck;
    public List<CardBartok> drawpile;
    public List<CardBartok> discardpile;

    void Awake()
    {
       S= this;
    }
    void Start()
    {
        deck = GetComponent<Deck>(); //Get the Deck 
        deck.InitDeck(deckXML.text); //pass DeckXML to it
        Deck.Shuffle(ref deck.cards); // this shuffles the deck 
        //The ref keyword passes a reference to deck.cards, which allows
        //deck.cards to be modified by Deck.shuffle()


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
