using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "SpawningObject")]
public class SpawningObject : ScriptableObject {

    new public string name;
    public float evilness;
    public GameObject gameObject;

}
