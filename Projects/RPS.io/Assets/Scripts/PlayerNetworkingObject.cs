using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkingObject : NetworkBehaviour {

    public Player player;

    [SerializeField]
    private GameObject playerUnit;

	void Start () {

        // Checking if it is actiually you executing this code
        if (!isLocalPlayer)
        {
            return;
        }
        // Spawning the player unit
        CmdSpawnPlayerUnit();
    }

    void Update () {
		
	}

    [Command]
    public void CmdSpawnPlayerUnit()
    {
        // Informing the game manager that a new player is spawned
        GameManager.instance.nrOfPlayers += 1;
        // Instantiating the playerunit
        GameObject go = Instantiate(playerUnit);
        // Spawning the playerunit
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
