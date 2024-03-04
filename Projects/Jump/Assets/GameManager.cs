using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public Upgrades upgrades;

    [SerializeField]
    private int numberOfObstacles = 10;
    public int NumberOfObstacles
    {
        get
        {
            return numberOfObstacles;
        }
        set
        {
            numberOfObstacles = value;
            levelText.text = NumberOfObstacles.ToString();
        }
    }

    public Material transparentCube;

    public Vector3 worldSize = Vector3.one * 20;

    public Transform obstacles;
    public Transform cubeCoins;
    public Transform centerOfCube;

    private float money = 0;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            scoreText.text = Money.ToString();
        }
    }
    
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;

    public GameObject cubeCoin;
    public GameObject normalWall;

    private void Start()
    {
        GenerateLevel();    
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < NumberOfObstacles; i++)
        {
            PlaceRandomObstacle();
        }

        void PlaceRandomObstacle()
        {
            Vector3 size = new Vector3(
                Mathf.Ceil(Random.value * (upgrades.maxCubeSize.x - upgrades.minCubeSize.x) + upgrades.minCubeSize.x), 
                Mathf.Ceil(Random.value * (upgrades.maxCubeSize.y - upgrades.minCubeSize.y) + upgrades.minCubeSize.y), 
                Mathf.Ceil(Random.value * (upgrades.maxCubeSize.z - upgrades.minCubeSize.z) + upgrades.minCubeSize.z));
            Vector3 position = new Vector3(
                Random.value * worldSize.x - worldSize.x / 2,
                Random.value * worldSize.y - worldSize.y / 2,
                Random.value * worldSize.z - worldSize.z / 2);

            GameObject go = Instantiate(normalWall, position, Quaternion.identity, obstacles);
            go.transform.localScale = size;
            go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }
}
