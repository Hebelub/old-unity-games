using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePhysics : MonoBehaviour {

    public PuzzlePhysicMaterial physics;

    MeshRenderer mr;
    Collider col;

    Color color;

    public PhysicMaterialCombine justForNowWillBeFixed;


	void Start () {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();

        physics = new PuzzlePhysicMaterial(Random.value, Random.value, Random.Range(0, 4));

        MaterialChanged();

    }
	
	public void MaterialChanged () {

        Debug.Log("PhysicsChanged");

        justForNowWillBeFixed = Combine.Material(Random.Range(0, 4));

        float margin = 0.5f;

        float h = Combine.Color(physics.combine); // Combine
        float s = margin + physics.bounciness / 2; // bounciness
        float v = margin + (1 - physics.friction) / 2; // Friction

        color = Color.HSVToRGB(h, s, v, true);

        mr.material.color = color;

        col.material.bounciness = physics.bounciness;
        col.material.dynamicFriction = physics.friction;
        col.material.staticFriction = physics.friction;
        col.material.bounceCombine = justForNowWillBeFixed;
        col.material.frictionCombine = justForNowWillBeFixed;

    }

    private bool waitingForUp = false;
    private Vector3 downPosition;

    private void Update()
    {
        if (waitingForUp && Input.GetMouseButtonUp(0))
        {
            waitingForUp = false;
            Vector3 change = (Input.mousePosition - downPosition) / 20;
            float xChange = change.x;
            float yChange = change.y;

            physics.bounciness += yChange;
            physics.friction += yChange;

            if (physics.bounciness > 1)
            {
                physics.bounciness = 1;
            }
            else if (physics.bounciness < 0)
            {
                physics.bounciness = 0;
            }
            if(physics.friction > 1)
            {
                physics.bounciness = 1;
            }
            else if (physics.bounciness < 0)
            {
                physics.bounciness = 0;
            }

            MaterialChanged();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Change combine on object");
        }
        if(Input.GetMouseButtonDown(0))
        {
            downPosition = Input.mousePosition;
            waitingForUp = true;
        }
    }

}

public class PuzzlePhysicMaterial
{
    public float friction;
    public float bounciness;
    public int combine;

    public PuzzlePhysicMaterial (float friction, float bounciness, int combine)
    {
        this.friction = friction;
        this.bounciness = bounciness;
        this.combine = combine;

    }
}

public static class Combine
{
    public static int average = 0;
    public static int minimum = 1;
    public static int multiply = 2;
    public static int maximum = 3;

    public static float Color (int combine) 
    {
        if (combine == 0)
        {
            return 120f / 360f;
        }
        else if (combine == 1)
        {
            return /*270f*/ 60f / 360f;
        }
        else if (combine == 2)
        {
            return 180f / 360f;
        }
        else if (combine == 3)
        {
            return 0f / 360f;
        }

        Debug.LogWarning("No such combine value: " + combine);
        return 0;
    }
    public static PhysicMaterialCombine Material (int combine)
    {
        if (combine == 0)
        {
            return PhysicMaterialCombine.Average;
        }
        else if (combine == 1)
        {
            return PhysicMaterialCombine.Minimum;
        }
        else if (combine == 2)
        {
            return PhysicMaterialCombine.Multiply;
        }
        else if (combine == 3)
        {
            return PhysicMaterialCombine.Maximum;
        }

        Debug.LogWarning("No such combine value: " + combine);
        return PhysicMaterialCombine.Average;
    }
}