using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public float spawnDistance = 15f;

    public SpawningObject[] listOfObjects;

    private GameManager gm;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        StartCoroutine(SpawnObjects());
	}
	
	void Update () {

	}

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            gm.evilness += gm.score * gm.difficulty * gm.spawnSpeed;

            SpawnRandomObject();

            yield return new WaitForSeconds(gm.spawnSpeed);
        }
    }

    private void SpawnRandomObject()
    {
        float evelness = gm.evilness;

        SpawningObject nextObject = listOfObjects[Random.Range(0, listOfObjects.Length)];

        if (nextObject.evilness < gm.evilness)
        {
            gm.evilness -= nextObject.evilness;
            SpawnObject(nextObject.gameObject);

        }

    }

    private void SpawnObject(GameObject go)
    {
        Instantiate(go, RandomSpawnPosition(), Quaternion.identity);
    }

    private Vector3 RandomSpawnPosition()
    {
        Vector2 random = Random.insideUnitCircle.normalized;

        return random * spawnDistance;
    }

}
