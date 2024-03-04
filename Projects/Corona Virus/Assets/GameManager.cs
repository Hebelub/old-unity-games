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

    // public GameObject[] objects = new GameObject[0];

    public GameObject virus;
    public GameObject cell;

    //public static Object GetRandomObject ()
    //{
    //    return instance.objects[Random.Range(0, instance.objects.Length)];
    //}

    private void Start()
    {
        //for (int i = 0; i < 200; i++)
        //{
        //    Vector3 pos = Random.insideUnitSphere * 15f;
        //    pos = new Vector3(Mathf.Ceil(pos.x), Mathf.Ceil(pos.y), Mathf.Ceil(pos.z));
        //    Instantiate(virus, pos, Quaternion.identity);

        //}

        //for (int i = 0; i < 100; i++)
        //{
        //    Vector3 pos = Random.insideUnitSphere * 750f;
        //    pos = new Vector3(Mathf.Ceil(pos.x), Mathf.Ceil(pos.y), Mathf.Ceil(pos.z));
        //    Instantiate(cell, pos, Quaternion.identity);

        //}

    }
}
