using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int width = 15;
    public int length = 15;
    public int height = 10;

    public GameObject stone;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                for (int k = 0; k < height; k++)
                {
                    if(Random.value <= 1f)
                        SpawnBlock(new Vector3(i, -k -1, j));
                }
            }
        }
    }

    public void SpawnBlock(Vector3 position)
    {
        Instantiate(stone, position, Quaternion.identity, transform);
    }
}
