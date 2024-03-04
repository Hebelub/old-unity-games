using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour {

    // Need to be done
    // 1. Camera                Done
    // 2. Build                 In progress
    // 3. Save / Load
    // 4. Undo / Redo
    // 5. More

    // Build:
    // 1. All components needs a square to click on so that we can place a component attached to it
    // All components could have a child with a 1*1*1 box on it wich detects click by mouse

    // Send a raycast, whatever compoonent it hits, it will place a block as a child of that component

    // This is what needs to happen:
    // 1. Select an component from a menu or in another way
    // 2. Click on another component to place it out, the new selected component should now be placed on clicked side. The new component is now a child of the clicked component
    // 3. Modyfy already exsisting components by rightcliking. Rightclicking an component opens a menu. Here you can change settings for component as well as type and simular
    // 4. When the whole vehicle is build, you can click on test, or you can save it:
    // Test will open the vehicle in a test area, now you can test the vehicle
    // Saving a vehicle costs the prise of the components and might be expensive
    // 5. You can load previously created vehicles. When you load a vehicle, it will replace any already existing vehicles on the build scene
    // 6. You did something wrong. You should be able to use ctrl + z to undo any action and ctrl + y to redo one
    // This might be achived by making a list of all actions and undo/redo them in order. (Other approaches might be used)
    // --> Vehicles witch is destroyed or damaged can be loaded from your vehicles. Here you can fix the vehicle wich will be less expensive than building it from scratch
    // Should be able to just click on a fix button, that button will fix the vehicle to the original saves design, and will cost the prise of the fixing
    // --> You should be able to delete saved vehicles and scrap/sell old owned vehicles
    // --> Components or subComponents might be locked, and is unlocked with more tecnology
    // --> Should be able to switch from build mode to mainMenu to level to sandbox mode by three tabs on the top (maby I will use a different approach, or add more tabs...)

    // Just an irrelevant idea, maby it chould be possible to drive on different planets and stuff (that means different gravity and drag)

    private Camera mainCamera;

    public float rayLength;
    public LayerMask layerMask;

    // This is the go that will be instantiated when building
    public GameObject selectedComponent;

    private const float unit = 1f;

    public static bool mustRaycastNextIteration;
    Vector3 lastMousePosition;

    public GameObject selected;
    public Transform buildTransform;

    RaycastHit hit;
    //MeshRenderer mesh;

	void Start ()
    {
        // Get the main Camera
        mainCamera = Camera.main;
    }

    void Update ()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (CheckMovement())
            {
                hit = SendRayFromCam();

                if (hit.collider == null)
                {
                    selected.SetActive(false);
                    buildTransform.gameObject.SetActive(false);
                }
                else
                {
                    selected.SetActive(true);
                    selected.transform.position = hit.collider.transform.position;
                    selected.transform.rotation = hit.collider.transform.rotation;
                    // Move BuildPosition to the build position
                    buildTransform.gameObject.SetActive(true);
                    MoveBuildPosition();
                }
            }

	        if (Input.GetMouseButtonDown(0))
            {
                BuildAt(hit);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Edit(hit);
            }
        }
	}

    private void MoveBuildPosition()
    {
        // Gets the point of where it hit
        Vector3 point = hit.point;
        // Gets the components transform
        Transform component = hit.collider.transform.parent;

        // Need to check what direction to build in, relative to the gameobject
        Vector3 relative = component.InverseTransformPoint(point);

        Vector3 buildDirection = GetBuildDirection(relative, component);
        Vector3 buildPosition = buildDirection + component.position * unit;

        Quaternion buildRotation = component.rotation;
        
        // Trying to find the rotation of the objrct.
        // Other things that should be tested is if there is enough space for the object, and if there is a connector on that direction on the object it is beeing attached to. Also the rotation is gonna be right compared to the component it is attached to!
   //     Instantiate(selectedComponent, buildPosition, Quaternion.Euler(buildDirection), component);

        buildTransform.position = buildPosition;
        buildTransform.rotation = buildRotation;
    }

    private bool CheckMovement()
    {
        if (mustRaycastNextIteration)
        {
            mustRaycastNextIteration = false;
            lastMousePosition = Input.mousePosition;
            return true;
        }
        else if (lastMousePosition != Input.mousePosition)
        {
            lastMousePosition = Input.mousePosition;
            return true;
        }

        return false;
    }

    private void BuildAt(RaycastHit hit)
    {
        // Check if hit has hit anything
        if (hit.collider != null)
        {
            // Trying to find the rotation of the objrct.
                // Other things that should be tested is if there is enough space for the object, and if there is a connector on that direction on the object it is beeing attached to. Also the rotation is gonna be right compared to the component it is attached to!
            Instantiate(selectedComponent, buildTransform.position, buildTransform.rotation, hit.collider.transform.parent);

            mustRaycastNextIteration = true;
        }
    }

    private void Edit(RaycastHit hit)
    {

    }

    private Vector3 GetBuildDirection(Vector3 relative, Transform hit)
    {
        Vector3 buildDirection;

        float xDis = Mathf.Abs(relative.x); // Gets the relative absolute distances
        float yDis = Mathf.Abs(relative.y);
        float zDis = Mathf.Abs(relative.z);

        // Check which one is longer and gets the build direction
        if (xDis > yDis && xDis > zDis)
        {
            // xDis is longest
            if (relative.x > 0)
                buildDirection = hit.right;
            else
                buildDirection = -hit.right;
         }
        else if (yDis > zDis)
        {
            // yDis is longest
            if (relative.y > 0)
                buildDirection = hit.up;
            else
                buildDirection = -hit.up;
        }
        else
        {
            // zDis is longest
            if (relative.x > 0)
                buildDirection = hit.forward;
            else
                buildDirection = -hit.forward;
        }

        return buildDirection;

    }

    private RaycastHit SendRayFromCam()
    {
        RaycastHit hit;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {

        }

        return hit;
    }

}
