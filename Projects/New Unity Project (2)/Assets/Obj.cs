using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public Entity everyObject;

    public GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        everyObject = GetComponent<Entity>();
    }

    public void DropItem(GameObject item, float distance)
    {
        Instantiate(item, transform.position + (Vector3)Random.insideUnitCircle * distance, Quaternion.identity);
    }
}
