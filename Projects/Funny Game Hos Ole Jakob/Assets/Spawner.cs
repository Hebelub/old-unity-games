using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Spawn enemy-spheres with random size
    // Random speed
    // Random color
    // Random type
    // Random interval
    // Random ...

    public Transform enemyFolder;
    public GameObject enemy;

    private void Start()
    {
        StartCoroutine(ISpawnSpheres());   
    }

    public IEnumerator ISpawnSpheres()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(RandomSpawnInterval());
        }

        float RandomSpawnInterval()
        {
            return Random.value * 1f;
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy, RandomEnemySpawnLocation(), Quaternion.identity, enemyFolder);
        newEnemy.transform.localScale = RandomEnemySize();
        newEnemy.GetComponent<Enemy>().enemyTarget = transform;

        Vector3 RandomEnemySpawnLocation()
        {
            return Random.onUnitSphere * 100;
        }
        Vector3 RandomEnemySize()
        {
            return Vector3.one * Random.value * 5 + Vector3.one * 1.0F; 
        }
    }


}
