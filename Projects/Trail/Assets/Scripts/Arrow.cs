using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Transform from;
    public Transform to;
    private AttractToMouse atm;
    private SpriteRenderer sr;

    void Start () {
        transform.GetChild(0).gameObject.SetActive(true);
        to = transform.root;
        atm = transform.root.GetComponent<AttractToMouse>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	void Update () {

        transform.position = (from.position - to.position) / 2 + to.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, to.position - transform.position);
        
        sr.color = new Color(1, 1 - 1 / atm.divider, 1 - 1 / atm.divider);

    }
}
