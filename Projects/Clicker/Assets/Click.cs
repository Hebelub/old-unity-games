using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public GameObject go;

    public Transform spawnAt;
	
	void Update () {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Random.Range(0, GameManager.Instance.spawnedItems + 1) == 0)
            {
                SpawnItem();
            }
        }
	}

    public void SpawnItem()
    {
        Instantiate(go, spawnAt.position, Random.rotation);
        GameManager.Instance.spawnedItems++;
    }
    
}
