using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singelton
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public Player player;

    public Goal[] goals;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            goals[0].NewPosition();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            goals[1].NewPosition();
        }

    }

    public void Restart()
    {
        // Should also restart other things then the player

        // Restarts the player
        player.transform.position = Vector3.up * 2;
        player.rb.velocity = Vector2.zero;
        player.rb.angularVelocity = 0f;
        player.transform.rotation = Quaternion.identity;
    }

}