using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform enemyTarget;

    public float movementSpeed;

    private void Start()
    {
        StartCoroutine(IMovement());
    }

    public IEnumerator IMovement()
    {
        while(true)
        {
            transform.Translate((-transform.position + enemyTarget.position) * movementSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
