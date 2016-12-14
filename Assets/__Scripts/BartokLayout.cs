using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//slotDef class is not based on Monobehaviour, so it doesn't need its own file. 
[System.Serializable] //Make slotDef able to be seen in the Unity Inspector
public class SlotDef
{
    public float x;
    public float y;
    public bool faceUp = false;
    public string layerName = "Default";
    public int layerID = 0;
    public int id;
    public List<int> hiddenBy = new List<int> (); // Unused in Bartok
    public float rot; // rotation of handle
    public string type = "slot";
    public Vector2 stagger;
    public int player; //player number  of a hand
    public Vector3 pos; //pos derived from x,y,& multiplayer
} 

public class BartokLayout : MonoBehaviour {
    public PT_XMLReader xmlr; //Just like Deck, this has a PT_XMLReader
    public PT_XMLHashtable xml; // this varibale is for faster xml access
    public Vector2 multiplier; // sets the spacing of the tableau
    //SlotDef referemces 
    public List<SlotDef> slotDefs; //the SlotDefs hands
    public SlotDef drawpile;
    public SlotDef discardpile;
    public SlotDef target;

    //This function is called to read in the LayoutXML.xml file
    public void ReadLayout(string xmlText)
    {
        xmlr = new PT_XMLReader();
        xmlr.Parse(xmlText); //The XMl is parsed
        xml = xmlr.xml["xml"][0]; //And is set as a shortcut to the XML

        //Read in the multiplier , which sets card spacing 
        multiplier.x = float.Parse(xml["multiplier"][0].att("x"));
        multiplier.x = float.Parse(xml["multiplier"][0].att("y"));


        //read in the slot 
        SlotDef tSD;
        //slot X is used as a shortcut to all the <slots>s
        PT_XMLHashList slotsX = xml["slot"];

        for (int i=0; i<slotsX.Count; i++)
        {
            tSD = new global::SlotDef(); // Create a new SlotDef instance 
            if (slotsX[i].HasAtt("type"))
            {
                //if this <slot> has a type atttibute parse it 
                tSD.type = slotsX[i].att("type");
            } else
            {
                //if not, set its type to "slot"
                tSD.type = "slot";
            }
            //Various attributes are parsed into numerical values 
            tSD.x = float.Parse(slotsX[i].att("x"));
            tSD.y = float.Parse(slotsX[i].att("y"));
            tSD.pos = new Vector3(tSD.x * multiplier.x, tSD.y * multiplier.y, 0);

            //sorting Layers
            tSD.layerID = int.Parse(slotsX[i].att("layer"));
            //In this game, the Sorting Layers are names 1,2,3,...through 10
            //This converts the number of the layerID into a text layerName
            tSD.layerName = tSD.layerID.ToString();
            //The layers are used to make sure that the correct cards are
            //on top of the others. In unity 2D, all of our assets are
            //to differentiate between them. 
            //pull additional attributes vased on the type of each <slot>
            switch (tSD.type)
            {
                case "slot":
                    //ignore slots that are just of the "slot" type 
                    break;

                case "drawpile":
                   discardPile= tSD;
                    break;

                case "hand":
                    //Information for each player's hand
                    tSD.player = int.Parse(slotsX[i].att("player"));
                    tSD.rot = float.Parse(slotsX[i].att("rot"));
                    slotDefs.Add(tSD);
                    break;

              
            }
            
            {

            }

        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
