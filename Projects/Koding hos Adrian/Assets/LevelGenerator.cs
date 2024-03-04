using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> canSpawn;

    public Vector2Int worldSize;

    public Transform objects;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                if(Random.value > 0.95f)
                    InstantiateRandomObject(new Vector3(i, 0, j));
            }

        }
    }

    public void InstantiateRandomObject(Vector3 position)
    {
        Debug.Log(position);
        var randomObject = canSpawn[Random.Range(0, canSpawn.Count)];
        Instantiate(randomObject, position + objects.position, Quaternion.identity);
    }



}
