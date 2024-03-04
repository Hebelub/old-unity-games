using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleSphere : MonoBehaviour {

    Mesh mesh;

	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
	}
	
	void Update () {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            mesh.vertices[i] *= Random.value * 2;



            // mesh.Clear();

            //mesh.vertices = vertices;
            //mesh.triangles = triangles;

            mesh.RecalculateNormals();

        }
	}
}
