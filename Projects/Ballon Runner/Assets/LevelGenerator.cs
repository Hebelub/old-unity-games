using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject spike;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < 1000; i++)
        {
            Instantiate(spike, Random.insideUnitSphere * (Random.value * 150f + 8f), Quaternion.identity);
        }
    }
}
