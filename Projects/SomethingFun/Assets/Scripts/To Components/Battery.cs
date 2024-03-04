using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : RocketComponent {

    public float capasity = 10;
    public float powerLeft = 10;
    private Rocket vehicleScript;

	void Start () {
        rootVehicle.powerLeft += powerLeft;
        rootVehicle.powerCapasity += capasity;
	}
	
	void Update () {
		
	}
}
