using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DetectKills : NetworkBehaviour {

    Player player;

    private void Start()
    {
        //if (!hasAuthority)
        //{
        //    return;
        //}

        player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (!hasAuthority)
        {
            return;
        }

        Player otherPlayer = collision.gameObject.GetComponent<Player>();

        int a = player.type;
        int b = otherPlayer.type;

        if (a == b)
        {
            // Tie
            Debug.Log("It is a tie");
            return;
        }
        //else if (a + 1 == b || a - 2 == b)
        //{
        //    // B wins
        //    Debug.Log(a + " wins over " + b);
        //    player.Die(otherPlayer);
        //    otherPlayer.Kill(player);
        //}

        // Else if you are the winning part
        else if (a - 1 == b || a + 2 == b)
        {
            // A wins
            Debug.Log(b + " wins over " + a);
            player.Kill(otherPlayer); // Make you the killer of the other player
        }
    }
}
