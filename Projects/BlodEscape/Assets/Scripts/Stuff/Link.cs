// An object with link on it, will inform the game manager about its existense, it is now possible to spawn objects on this position via the game manager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    
    GameManager gm;

	void Start ()
    {
        // Assigning the gm
        gm = GameManager.instance;
        // Informing the gm that this link is in existense
        InformGameManager();
	}
	
    public void InformGameManager()
    {
        // Adding this to the gm.links list
        gm.links.Add(this);
    }

}
