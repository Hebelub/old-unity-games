using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public Levels levels;

    public float score = 0;
    public float coins = 0;
    public float stamina = 0;
    public float health = 3;
    public int level = 0;
    public GameObject player;

    public GameObject ground;
    public Transform negativeCorner;

	void Start () {

    }
	
	void Update () {
		
	}

}
