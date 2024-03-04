using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkObject : NetworkBehaviour
{

    public GameObject playerPrefab;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Debug.Log("Returning because it is not the local player");
            return;
        }

        CmdSpawnPlayer();

    }

    void Update()
    {

    }

    [Command]
    public void CmdSpawnPlayer()
    {
        Debug.Log("CmdSpawnPlayer is running");
        GameObject go = Instantiate(playerPrefab);

        NetworkServer.Spawn(go);
    }



}
