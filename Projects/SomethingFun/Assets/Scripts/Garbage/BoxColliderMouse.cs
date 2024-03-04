using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderMouse : MonoBehaviour {

    private MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

}
