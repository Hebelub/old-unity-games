using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {

    private float maxSize = 8f;

    public Vector2 playerGrowSize;

    Levels levels;

    GameObject player;

    bool isBigger;

    MeshRenderer mr;

    private void Start()
    {
        mr = transform.GetComponent<MeshRenderer>();
        player = GameManager.instance.player;
        levels = GameManager.instance.levels;
        float xSize = Random.Range(1, maxSize);
        float zSize = Random.Range(1, maxSize);
        playerGrowSize = new Vector2(xSize, zSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerControls controlls = other.gameObject.GetComponent<PlayerControls>();
            Vector3 resizeTo = new Vector3(playerGrowSize.x, other.transform.localScale.y, playerGrowSize.y);
            controlls.ResizePlayer(resizeTo);
            levels.currentItems[0]--;
            if (levels.currentItems[0] == 0)
                levels.NextLevel();
            Destroy(gameObject);
            
        }
    }

    private void Update()
    {
        bool wasBigger = isBigger;

        float area = player.transform.localScale.x * player.transform.localScale.z;
        float cheeseArea = playerGrowSize.x * playerGrowSize.y;

        if (area < cheeseArea)
        {
            isBigger = true;
        }
        else
        {
            isBigger = false;
        }

        if (isBigger != wasBigger)
        {
            if (isBigger)
            {
                mr.material.color = new Color(1f, 1f, 0.07058824f);
            }
            else
            {
                mr.material.color = new Color(1f / 3f, 1f, 0.07058824f);
            }
        }

    }

}
