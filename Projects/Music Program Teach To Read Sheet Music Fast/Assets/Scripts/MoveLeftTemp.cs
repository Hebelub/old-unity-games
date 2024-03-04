using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftTemp : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(IMoveLeft());
    }

    public IEnumerator IMoveLeft()
    {
        while(true)
        {
            transform.Translate(Vector3.left * C.noteMoveSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
