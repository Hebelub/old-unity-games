using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassPosition {

    public float mass;
    public Vector3 position;

    public MassPosition(float mass, Vector3 centerOfMass)
    {
        this.mass = mass;
        this.position = centerOfMass;
    }

}
