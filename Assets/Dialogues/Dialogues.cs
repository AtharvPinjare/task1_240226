using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Gives On Editor Access

//This Script will contain all the info of a single dialogue appearing on the sceen.
//Thus it is not a monobehaviour
public class Dialogues
{
    public string name; //Name of the user.

    [TextArea(3,10)]//Defines how big your text box should be.
    public string[] sentences;//Sentences inside the Dialogue.
}
