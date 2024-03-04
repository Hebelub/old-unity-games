using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Obj
{
    public List<GameObject> storage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>()._name == everyObject._name)
        {
            if (collision.gameObject.GetComponent<Entity>().id < everyObject.id)
            {
                return;
            }
        }
        if (collision.gameObject.GetComponent<Entity>().isItem)
        {
            StoreItem(collision.gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OpenBag();
        }
    }

    public void OpenBag()
    {
        foreach (GameObject go in storage)
        {
            go.SetActive(true);
            // DropItem(go, 3f);
            // Destroy(go);
            go.transform.position = transform.position + (Vector3)Random.insideUnitCircle.normalized * (transform.localScale.magnitude + 0.1f);
        }

        storage = new List<GameObject>(0);
        UpdateScale();

    }

    public void UpdateScale()
    {
        transform.localScale = Vector3.one + Vector3.one * 0.1f * storage.Count;
    }

    public void StoreItem(GameObject itemToStore)
    {
        storage.Add(itemToStore);
        itemToStore.SetActive(false);
        UpdateScale();
    }

}
