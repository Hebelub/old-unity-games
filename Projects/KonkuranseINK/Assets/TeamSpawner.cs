using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSpawner : MonoBehaviour
{
    public Team team;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = team.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 16.0F);
    }

    private void OnMouseDown()
    {
        GameManager.instance.SpawnPoint(team);
    }
}
