using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controlls : NetworkBehaviour {

    Player player;

    public float movingSpeed = 0.1f;

    private void Start()
    {
        // Defining player
        player = GetComponent<Player>();
        
        if (!hasAuthority)
        {
            return;
        }
    }

    public bool justABool = false;

    private void Update ()
    {

        // Checking if you are running the code
        if (!hasAuthority)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            justABool = !justABool;  
        }
        if (!justABool)
        {
            return;
        }
        // Will change the type if the player
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeTypeToRelative(1);

        }
        
        // Will follow your curser
        transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), movingSpeed * Time.deltaTime);
	}
}
