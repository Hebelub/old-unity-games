using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlot : MonoBehaviour {

    public Power powerUp;

    void Start () {
        // Adding itselves to the powerUpSlots list in gameManager
        GameManager.instance.powerUpSlots.Add(gameObject);

        SetPower();
    }

    void Update () {
		
	}

    public void SetPower()
    {
        if (transform.childCount > 1)
            powerUp = transform.GetChild(1).GetComponent<Power>();
        Debug.Log("Power is set to: " + powerUp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            if (transform.childCount > 1) // Means that the slot contains a powerUp
            {
                PickedUp(other);        
            }
        }
    }

    public void PickedUp(Collider player)
    {

        Player playerScript = player.transform.parent.GetComponent<Player>();

        if (powerUp.hasInstaEffect)
        {
            powerUp.InstaEffect(playerScript);
        }

        if (powerUp.hasEffect)
        {
            playerScript.powerUp = powerUp;
        }

        // Removes the powerUp from the powerSlot
        Destroy(transform.GetChild(1).gameObject);

        if (transform.childCount > 2) // Means that the slot contains a powerUp
        {
            Debug.LogWarning("There was " + transform.childCount + " power ups in this power up slot");
        }

        GameManager.instance.emptyPowerUpSlots.Add(gameObject);
    }

}

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public float spawnProbapillity = 1f;
    public GameObject gameObject;
    public string powerUp;

}