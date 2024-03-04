using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplodeOnClick : MonoBehaviour {

    public float rayLength;
    public LayerMask layerMask;

    public float explotionRadius = 5f;
    public float explotionPower = 10f;
    public float explotionLift = 1;

    Vector3 explosionPos;

    Camera cam;

    bool somethingBlocking = false;

	void Start () {

        cam = Camera.main;

    }
	
	void Update () {

        bool leftMouseDown = Input.GetMouseButtonDown(0);

        somethingBlocking = EventSystem.current.IsPointerOverGameObject();

        if (leftMouseDown && !somethingBlocking)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                Explode(hit);
            }
        }
	}

    public void Explode(RaycastHit hit)
    {
        explosionPos = hit.point;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, explotionRadius);
        foreach (Collider affected in colliders)
        {
            //explosionPos.y = affected.transform.position.y;

            Rigidbody rb = affected.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explotionPower, explosionPos, explotionRadius, explotionLift);
        }

    }
}
