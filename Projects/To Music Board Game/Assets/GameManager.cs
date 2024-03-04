using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //Awake is always called before any Start functions
    void Awake()
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

    public Text surivivalTimeText;

    public float survivalTime = 0f;

    public Vector3 grid;

    public GameObject groundTile;

    public float squareSize = 1f;

    public float countSurvivalTimeEvery = 1f;

    void Start () {
        CreateGround();
        StartCoroutine(Timer());
    }

    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            survivalTime = 0f;
            surivivalTimeText.text = survivalTime.ToString();
        }
	}

    public IEnumerator Timer()
    {
        while (true)
        {
            surivivalTimeText.text = survivalTime.ToString();

            yield return new WaitForSeconds(countSurvivalTimeEvery);

            survivalTime += countSurvivalTimeEvery;

        }

    }

    public void CreateGround()
    {
        float x = grid.x;
        float y = grid.y;
        float z = grid.z;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k <= z; k++)
                {
                    Instantiate(groundTile, new Vector3(i, j, k) * squareSize, Quaternion.identity);
                }
            }
        }
    }

}
