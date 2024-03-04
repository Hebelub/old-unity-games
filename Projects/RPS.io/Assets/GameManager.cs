using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour { // It might not need to derive from networkbehavior !---!

    #region Singelton

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int nrOfPlayers;

    // When a player changes type it must inform the gameManager about that
    public int nrOfType0;
    public int nrOfType1;
    public int nrOfType2;

    public Vector2 mapSize; // Keeps information about the size of the map and when this is changed the size of thae map will also change corespondingly

    

    public void SpawnPlayer()
    {
        // Spawning player for the player asking. The type should be the one with the least nrOftype so that the always is an close to equal ammount of types
    }

}
