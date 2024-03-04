using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private float damage = 1;

    Levels levels;

    private void Start()
    {
        levels = GameManager.instance.levels;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.health -= damage;
            levels.currentItems[1]--;
            Debug.Log("Your health is now: " + GameManager.instance.health);
            Destroy(gameObject);

        }
    }

}
