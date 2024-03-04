using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateLevel();
    }

    public Transform spawnParent;
    public GameObject[] balancingGos;

    public float nrOfObjectsOnLevel;

    public int nrOfPlayers;

    public void CreateLevel()
    {
        for (int i = 0; i < nrOfObjectsOnLevel; i++)
        {
            if(Random.Range(0, 4) == 0)
            {
                InstantiateRandomObjectAtPosition(i);

            }
        }
    }
    private void InstantiateRandomObjectAtPosition(int atPosition)
    {
        int randomObject = Random.Range(0, balancingGos.Length);

        Instantiate(balancingGos[randomObject], spawnParent.GetChild(atPosition).position + Vector3.up * 2, Quaternion.identity);
    }

}
