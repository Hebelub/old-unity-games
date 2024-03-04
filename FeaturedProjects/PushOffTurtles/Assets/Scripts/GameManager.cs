using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


    public List<GameObject> players;
    public List<Transform> spawnpoints;
    public List<GameObject> powerUpSlots;

    public List<GameObject> emptyPowerUpSlots;

    // Should be a list of all the power ups
    public PowerUp[] powerUps;
    
	void Start () {
        if (players.Count > spawnpoints.Count)
        {
            Debug.LogWarning("There is more players then number of spawnpoints");
            // Should create a new spawnpoint on a desent location

        }

        emptyPowerUpSlots = powerUpSlots;

        // should give every player a spawnpoint
        int i = 0; // Should be given the spawnpoints randomly (just maby?)
        foreach (GameObject go in players)
        {
            Player script = go.GetComponent<Player>();
            script.spawnpoint = spawnpoints[i];
            i++;
        }

        RestartGame();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnRandomPower();
        }

	}

    public void SpawnRandomPower()
    {
        int slotAmmount = emptyPowerUpSlots.Count;
        int powerUpAmmount = powerUps.Length;

        if (slotAmmount > 0 && powerUpAmmount > 0)
        {

            int spawnAt = Random.Range(0, slotAmmount);
            int spawnIndex = Random.Range(0, powerUpAmmount);

            Debug.Log("Spawn at: " + spawnAt + ", spawn index: " + spawnIndex + ", power up ammount: " + slotAmmount + ", spawn at: " + powerUpAmmount);

            // PowerUpSlot powerUpSlot = powerUpSlots[spawnAt].GetComponent<PowerUpSlot>();

            SpawnPower(spawnAt, spawnIndex);
        }

    }

    private void SpawnPower(int spawnAt, int spawnIndex)
    {
        Transform parent = powerUpSlots[spawnAt].transform;
        Debug.Log("Parent: " + parent);
        PowerUp powerUp = powerUps[spawnIndex];

        Instantiate(powerUp.gameObject, Vector3.zero, Quaternion.identity, parent);

        emptyPowerUpSlots.RemoveAt(spawnAt);

        parent.gameObject.GetComponent<PowerUpSlot>().SetPower();
    }

    public void RestartGame()
    {
        // Reset the stadio
        // Use foreach to reset all players to spawn position
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().Restart();
        }
    }

}
