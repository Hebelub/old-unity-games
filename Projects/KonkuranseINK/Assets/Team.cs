using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public int id;
    public static int nextId = 0;

    // public GameObject pointObject;

    private void Awake()
    {
        id = nextId++;

    }

    private void Start()
    {
        GameManager.instance.allPoints.Add(this);
    }

    public int playerTeam = 0;

    private void OnMouseDown()
    {
        Destroy(gameObject);
        // GameManager.instance.SpawnPoint(this, 1);
    }

    public Vector3 positionWhenMoved = Vector3.zero;
}
