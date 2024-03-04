using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutside : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        GameManager.Instance.spawnedItems--;
    }
}
