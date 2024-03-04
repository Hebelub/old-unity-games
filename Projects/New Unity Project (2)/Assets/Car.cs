using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Alive
{
    public GameObject showingDriver;

    public bool hasDriver = false;

    public GameObject driver;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();

        if (entity._name == "Human")
        {
            if (hasDriver == false)
            {
                hasDriver = true;

                driver = entity.gameObject;
                showingDriver.SetActive(true);
                entity.gameObject.SetActive(false);

                StartCoroutine(IWalk());
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (hasDriver)
            {
                hasDriver = false;
                StopAllCoroutines();

                driver.SetActive(true);
                showingDriver.SetActive(false);

                driver.transform.position = transform.position + (Vector3)Random.insideUnitCircle.normalized * (transform.localScale.magnitude + 0.1f);
                driver = null;
            }
        }
    }

}
