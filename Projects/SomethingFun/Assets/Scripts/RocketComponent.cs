using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketComponent : MonoBehaviour {

    public int id = 0;
    public KeyCode[] input;

    // Should contain things that all components have, for example wind resistance
    public float mass = 0;
    public Vector3 centerOfMass = Vector3.zero;

    public Transform rootTransform;
    public Rigidbody rootRigidbody;
    public Rocket rootVehicle;

    private void OnEnable()
    {
        Rocket.OnVehicleSpit += ReRoot;
    }
    private void OnDisable()
    {
        Rocket.OnVehicleSpit -= ReRoot;
    }

    private void ReRoot ()
    {
        rootTransform = transform.root;
        rootRigidbody = rootTransform.GetComponent<Rigidbody>();
        rootVehicle = rootTransform.GetComponent<Rocket>();
    }

}
