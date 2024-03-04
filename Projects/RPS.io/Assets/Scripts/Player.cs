using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    // Type: Rock, paper or sissors)
    public int type = 0; // 0 = rock, 1 = paper, 2 = sissors

    // The number of different types that exists
    public int typeAmmount = 3; // This variable should be in the gamemanager that does not exist yet!!!

    // The gameobjects of the types
    public GameObject rock, paper, sissors;

    void Start() {
        if (!hasAuthority)
        {
            return;
        }
        ChangeTypeToRandom();
    }

    public void ChangeTypeToRandom()
    {
        // Getting a random type
        int newType = Random.Range(0, typeAmmount);
        // Loging something
        Debug.Log(hasAuthority);
        // Setting the random type
        CmdChangeTypeTo(newType); // There is something wrong here
    }

    public void ChangeTypeToRelative(int change)
    {
        CmdChangeTypeTo(type + change);
    }
    
    [Command]
    public void CmdChangeTypeTo(int type) // This must happen on all clients
    {
        // Just some easy corection
        if (type < 0)
        {
            type %= typeAmmount;
            type += typeAmmount;
        }
        else if (type >= typeAmmount)
        {
            type %= typeAmmount;
        }
        // Switching on all clients to
        RpcChangeTypeTo(type);

    }
    [ClientRpc]
    public void RpcChangeTypeTo(int type)
    {
        // Setting the global type variable
        this.type = type;

        // Disabelig all types
        rock.SetActive(false);
        paper.SetActive(false);
        sissors.SetActive(false);
        // Changing type to one of the following
        switch (type)
        {
            case 0:
                rock.SetActive(true);
                break;
            case 1:
                paper.SetActive(true);
                break;
            case 2:
                sissors.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Kill(Player victim)
    {
        Debug.Log("You killed " + victim);
        CmdChangeTypeTo(victim.type);

        // Geting the networkidentity component                                         //////////////// Fix this
        //////NetworkIdentity networkIdentity = victim.GetComponent<NetworkIdentity>();
        //////networkIdentity.AssignClientAuthority(connectionToClient);
        victim.Die(this); // Make the victim die
        //////networkIdentity.RemoveClientAuthority(connectionToClient);
        
    }
    public void Die(Player killer)
    {
        Debug.Log("You were killed by " + killer);
        ChangeTypeToRandom();

        //// Moving object to random position on screen
        //float spawnY = Random.Range
        //        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        //float spawnX = Random.Range
        //    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        //Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        //
        //transform.position = spawnPosition;

        transform.position = Vector3.zero;

    }
}
