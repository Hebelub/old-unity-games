using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Name of the game could be something like: "Squashy Square"

public class Levels : MonoBehaviour
{

    /* 
     * 0 = Cheese
     * 1 = Bomb
     * 2 = Trap
     * 3 = Wall
     * 4 = PowerUp
     * 5 = Coin
     * 6 = Box
     */

    public GameObject[] items;

    private int[] spawnAmmounts;

    public int[] currentItems;

    private int numberOfItems;

    void Start()
    {
        ground = GameManager.instance.ground;
        negativeCorner = GameManager.instance.negativeCorner;

        numberOfItems = items.Length;
        spawnAmmounts = new int[numberOfItems];
        currentItems = new int[numberOfItems];

        for (int i = 0; i < items.Length; i++)
        {
            spawnAmmounts[i] = 2;
        }

        NextLevel();
    }

    void Update()
    {

    }

    public void NextLevel()
    {

        int spawnAmmount = 0;
        int[] spawnDeck;

        IncreaseAmmounts();

        for (int i = 0; i < numberOfItems; i++)
        {
            spawnAmmount += spawnAmmounts[i] - currentItems[i];
        }
        spawnDeck = new int[spawnAmmount];
        {
            int k = 0;
            for (int i = 0; i < numberOfItems; i++)
            {
                for (int j = 0; j < spawnAmmounts[i] - currentItems[i]; j++)
                {
                    spawnDeck[k] = i;
                    k++;
                }
            }
        }

        for (int i = 0; i < spawnAmmount; i++)
        {
            Spawn(spawnDeck[i]);
        }

    }

    private void IncreaseAmmounts()
    {
        for (int i = 0; i < items.Length; i++)
        {
            spawnAmmounts[i] += 1;
        }
    }

    public float spawnHeight = 4f;
    private GameObject ground;
    private Transform negativeCorner;

    public void Spawn(int item)
    {
        Vector3 spawnArea = ground.transform.localScale;

        float spawnX = Random.Range(0.5f, spawnArea.x - 0.5f);
        float spawnZ = Random.Range(0.5f, spawnArea.z - 0.5f);

        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, spawnZ);

        Instantiate(items[item], spawnPosition + negativeCorner.position, Quaternion.identity);

        // If successfull:
        currentItems[item]++;

    }

}