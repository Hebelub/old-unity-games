using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

	void Start () {
        GameManager.instance.spawnpoints.Add(transform);
	}
	
}
