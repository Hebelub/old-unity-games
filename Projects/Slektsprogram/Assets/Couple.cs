using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couple : MonoBehaviour
{
    public List<Person> parents;
    public List<Person> children;

    private LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        SomeFunction();
    }

    private void SomeFunction()
    {
        int i = 0;

        foreach (Person parent in parents)
        {
            Debug.Log(i);
            lr.SetPosition(i++, transform.position);
            Debug.Log(i);
            lr.SetPosition(i++, parent.transform.position);
        }
        foreach (Person child in children)
        {
            lr.SetPosition(i++, transform.position);
            lr.SetPosition(i++, child.transform.position);
        }
    }
}
