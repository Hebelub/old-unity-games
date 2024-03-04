using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : RocketComponent {

    public GameObject vehiclePrefab;

    public float detachForce = 0;

	void Update () {

        if (Input.GetKeyDown(input[0]))
        {
            GameObject instantiatedVehicle = Instantiate(vehiclePrefab);

            // Detaches from parent
            transform.parent = instantiatedVehicle.transform.GetChild(0);

            Rocket.VehicleSplit();

            rootVehicle.RedoVehicleCalculations();

            // Need to find center of mass
            // Need to attach vehicle to it
            // All childs need to get the childs root

            // 1. Add a vehicle compoenent
            // 2. Make newVehicle as parent of dropper
            // 3. Calculate center of mass on both vehicles
            // 4. All childs of this component needs to reroot
            // 5. Add a force in the detach direction

        }

	}
}
