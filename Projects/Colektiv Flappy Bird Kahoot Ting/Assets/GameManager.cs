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

    public float colectiveFraction = 0f;

    public KeyCode[] keys;

    private void Update()
    {
        int clicks = 0;

        foreach (KeyCode key in keys)
        {
            if (Input.GetKey(key))
            {
                clicks += 1;
            }

        }

        colectiveFraction = (float)clicks / keys.Length;

        Debug.Log(clicks + " cf: " + colectiveFraction + "sdfgsdf" + keys.Length);

    }
}
