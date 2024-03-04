using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
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

    public bool isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetMouseButtonDown(0))
        {
            SpawnRandomObstacle();
        }
        
    }

    public GameObject player;
    
    public void Restart ()
    {
        // Making the gameover text invisible
        gameOver.SetActive(false);

        // Destroys all gameobjects in the lung transform
        foreach (Transform child in lung.transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }

        player.GetComponent<Oxygen>().Respawn();

        AddScore(-score);

        player.GetComponent<Oxygen>().AddHealth(0f);

    }

    private void Start()
    {
        CreateGrid();
        StartCoroutine(ISpawnObstacles());
    }

    public Lung lung;
    
    // All obstacles that is possible to instantiate
    public List<GameObject> obstacles;

    public GameObject gridPoint;

    public float spacing = 1f;

    public Vector2Int gridSize = new Vector2Int(10, 10);

    public GameObject gameOver;

    public float spawnedOxygenAbsorbtionRelativeToOxygen = 0f;

    public float score = 0f;

    public IEnumerator ISpawnObstacles()
    {
        while (!isGameOver)
        {
            // Spawning a random obstacle every iteration
            SpawnRandomObstacle();

            if (spawnedOxygenAbsorbtionRelativeToOxygen > 1)
            {
                //SpawnOxygen();

                spawnedOxygenAbsorbtionRelativeToOxygen -= 1;

            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public Transform oxygenStartSpawnPosition;
    public GameObject oxygenGo;
    public void SpawnOxygen()
    {
        // Spawn oxygen into the world
        Instantiate(oxygenGo, oxygenStartSpawnPosition.position, Quaternion.identity, lung.transform);

        
    }

    public void SpawnRandomObstacle()
    {
        // Geting the spawn position
        Vector3 spawnPosition = GetRadomPointOnGrid();

        // Geting the random gameObject to instantiate
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];

        // Instantiating the given object at given position
        Instantiate(obstacle, spawnPosition, Quaternion.identity, lung.transform.GetChild(1));

        // Add a point
        AddScore(1f);
    }

    public Text scoreText;

    public int nrOfDigitsInScore;

    //public TextMesh scoreText;
    public void AddScore(float ammount)
    {
        score += ammount;
        string newScoreText;
        string scoreString = score.ToString();
        int scoreStringLength = scoreString.Length;

        newScoreText = scoreString;

        while (scoreStringLength - nrOfDigitsInScore < 0)
        {
            newScoreText = "0" + newScoreText;
            scoreStringLength++; 
        }

        scoreText.text = newScoreText;
    }

    public Vector3 GetRadomPointOnGrid()
    {

        Vector3 randomPoint;
        do
        {
            randomPoint = new Vector3(Random.Range(0, gridSize.x) - gridSize.x / 2, Random.Range(0, gridSize.y) - gridSize.y / 2) * spacing;
        }
        while (!VectorInAllowedPosition(randomPoint, 4));

        return randomPoint;

    }

    public bool VectorInAllowedPosition(Vector3 vector, float distanceAllowed)
    {
        bool isToClose;

        var positionOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y);

        isToClose = ((positionOfPlayer - vector).magnitude < /*The gap between player and spawning*/ distanceAllowed) ? false : true;

        return isToClose;
    }

    private void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Instantiate(gridPoint, new Vector3(i - gridSize.x / 2, j - gridSize.y / 2, 0) * spacing, Quaternion.identity, lung.transform.GetChild(0));
            }
        }
    }

    public Vector2Int PositionToCoord(Vector3 position)
    {
        position /= spacing;

        //position += Vector3.one / 2;

        // Rounding the vector to the nearest position on the grid
        Vector2Int coords = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

        return coords;
    }

}
