using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    // Trying to make the OnEventThing

    public delegate void SplitAction();
    public static event SplitAction OnVehicleSpit;

    public static void VehicleSplit()
    {
            if (OnVehicleSpit != null)
                OnVehicleSpit();
    }

    // End

    public float powerLeft = 0;
    public float powerCapasity = 0;

    // Physics stuff
    public float vehicleMass;
    public Vector3 centerOfMass;

    public List<MassPosition> massPositions = new List<MassPosition>();

	void Start ()
    {
        VehicleSplit();
        NewVehicleCalculate();
    }

    // Should be runed when a new vehicle is made or when one wihecle is split in two parts, the new part now needs vehicle calculations
    public void NewVehicleCalculate()
    {
        RedoVehicleCalculations();
    }

    // Should be runned when vehicle is split in two in any way
    public void RedoVehicleCalculations()
    {
        // Calculates the center of mass and the mass of the vehicle
        CalculateCenterOfMass();
        // Should rerot all child components

    }

    // Should be runned when center of mass is changed, 1. When components changes transform, 2. When vihecle is split in two parts, 3. maby in more situations
    public void CalculateCenterOfMass()
    { // it need to be calculated relative to vehicle.GetChild(0);
        Transform vehicle = transform;

        FindChildMassCenter(vehicle);
        MassPositionsToCOM(massPositions);

        Debug.Log("The mass is " + vehicleMass + " and the center of mass is " + centerOfMass);

        Transform childOfVehicle = vehicle.GetChild(0);
        Vector3 tempPosition = childOfVehicle.position;
        vehicle.position = centerOfMass;
        childOfVehicle.position = tempPosition;

    }

    private void FindChildMassCenter(Transform component)
    {

        Transform childOne = component.GetChild(0);

        // Make a list of all gameobjects with a mass component
        int childQuantity = childOne.childCount;

        for (int i = 0; i < childQuantity; i++)
        {
            Transform childComponent = childOne.GetChild(i);

            RocketComponent info = childComponent.GetComponent<RocketComponent>();
            Vector3 position = childComponent.position;

            Vector3 comoc = info.centerOfMass; // Center Of Mass In Component

            // Theese two values is needed to calculate canter of mass
            float mass = info.mass;
            Vector3 positionInWorldSpace = /*childComponent.right * comoc.x +
                                           childComponent.up * comoc.y +
                                           childComponent.forward * comoc.z +*/
                                           position;

            Debug.Log("The position is : " + positionInWorldSpace + ", and the mass is: " + mass);

            MassPosition massPosition = new MassPosition(mass, positionInWorldSpace);

            massPositions.Add(massPosition);

            FindChildMassCenter(childComponent);

        }

    }

    // Returns center of mass out of the massPositions list
    private void MassPositionsToCOM(List<MassPosition> massPositions)
    {

        Vector3 massPositionSum = Vector3.zero;
        float massSum = 0;

        foreach (MassPosition mp in massPositions)
        {
            massPositionSum += mp.mass * mp.position;

            massSum += mp.mass;
        }

        Vector3 centerOfMass = massPositionSum / massSum;

        this.centerOfMass = centerOfMass;
        this.vehicleMass = massSum;

        Debug.Log("Mass position sum: " + massPositionSum + ", mass sum " + massSum);

    }

}
