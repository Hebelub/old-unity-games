using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 (0,0,0), // 0
            new Vector3 (0,0,1), // 1
            new Vector3 (1,0,0), // 2
            new Vector3 (1,0,1), // 3
            
            new Vector3 (0,1,0), // 4
            new Vector3 (0,1,1), // 5
            new Vector3 (1,1,0), // 6
            new Vector3 (1,1,1)  // 7
        };

        triangles = new int[]
        {
            2, 1, 0,
            1, 2, 3,

            0, 1, 4,
            4, 1, 5,

            0, 4, 2,
            2, 4, 6,

            4, 5, 6,
            7, 6, 5,

            2, 6, 3,
            3, 6, 7,

            3, 5, 1,
            7, 5, 3

        };

    }

    private void Update()
    {
        //vertices[Random.Range(0, vertices.Length)].y += +.1f;


        //vertices[Random.Range(0, vertices.Length)] += Random.insideUnitSphere * 10;
        //  
        //UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
