using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Inventory inventory;

    public TaskObject taskObject;

    public Boat boat;

    public GameObject findingDrop;
    public GameObject islandLooker;

    public AudioSource audioSource;

    public List<Finding> findings;
    public List<Island> islands;
    public Transform islandFolder;

    // public Text scoreText;
    public float score = 0;
    public float health;
    public float streak;
    public float gameScore;

    public HealthBar scoreBar;

    public GameObject[] islandsToSpawn;

    public GameObject showOnWin;
    public GameObject showOnLoose;

    public SpriteRenderer slotForPicture;

    public HealthBar healthBar;

    public Transform exploreCardTransform;

    public LastFinding lastFinding;
    public Finding explodedHole;
    public AudioClip explotionSound;

    public AudioClip win;
    public AudioClip loose;

    public void RemoveLastFinding()
    {
        lastFinding.island.map[lastFinding.index] = explodedHole;
        lastFinding.island.ShowCard(explodedHole);
    }

    private void Start()
    {
        Reset();
        
        //inventoryAmounts = new int[4];
        //inventoryTexts = new TextMeshProUGUI[4];
    }
    
    //public IEnumerator IShowGameObject(GameObject go)
    //{
    //    bool b = true;
    //    while (b)
    //    {
    //        go.SetActive(true);

    //        b = false;
    //        yield return new WaitForSeconds(3f);
    //    }

    //    go.SetActive(false);
    //}

    private GameObject wasActiveLast;

    public List<BuyButton> shop;

    public void UpdateUi()
    {
        // scoreText.text = gameScore.ToString();
        healthBar.NewHealth(health);
        scoreBar.NewHealth(score);
        poison = poisonBar.SpawnChildren(poison);
        streak = crownBar.SpawnChildren(streak);

        healthText.SetText(health.ToString());
        scoreText.SetText(score.ToString());

        foreach (BuyButton bb in shop)
        {
            bb.UpdateStuff();
        }

        //if (score > scoreBar.full)
        //{
        //    Win();
        //}

    }

    public TextMeshProUGUI toolTips;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI cardText;

    public PoisonBar poisonBar;
    public PoisonBar crownBar;

    public bool possibleToExplore = true;

    public Finding deadWolf;
    public Finding treasure;

    //public void MoveCardToPosition()
    //{
    //    //Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //Vector3 offset = Vector3.right * -4f;

    //    //exploreCardTransform.position = mouse + offset + Vector3.forward * 10f;
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (possibleToExplore)
                Die();
        }

        if (health < 0)
        {
            Die();
        }
        else if (score >= scoreBar.full)
        {
            Win();
        }
    }

    public float poison;

    public void ShowWinOrLoose(GameObject go)
    {
        wasActiveLast = go;
        wasActiveLast.SetActive(true);
    }

    public void Die()
    {
        //StartCoroutine(IShowGameObject(showOnLoose));
        ShowWinOrLoose(showOnLoose);

        streak = 0;
        audioSource.PlayOneShot(loose, 1f);
        Reset();
    }
    
    public void Win()
    {
        //StartCoroutine(IShowGameObject(showOnWin));
        ShowWinOrLoose(showOnWin);

        streak += 1;
        gameScore += streak;
        audioSource.PlayOneShot(win, 1f);
        Reset();
    }

    public void Reset()
    {
        score = 0f;
        health = 20f;
        poison = 0f;
        UpdateUi();

        ExploreNewIslands();

        //foreach (Island island in islands)
        //{
        //    island.CreateDeck();
        //}
    }

    public float exploreDistance = 30;
    public float exploreSpeed = 0.5f;

    public void ExploreNewIslands()
    {
        if (possibleToExplore)
        {
            RemoveIslands();
            GenerateIslands(Random.Range(4, 7));

            StartCoroutine(IMoveIslands());
        }
    }

    public IEnumerator IMoveIslands()
    {
        possibleToExplore = false;

        for (int i = 0; i < exploreDistance / exploreSpeed; i++)
        {
            foreach (Transform island in islandFolder)
            {
                island.position += Vector3.left * exploreSpeed;

            }

            yield return null;
        }

        possibleToExplore = true;

        if (wasActiveLast != null)
            wasActiveLast.SetActive(false);
    }

    public void Nuke()
    {
        foreach (Island island in islands)
        {
            island.Bomb();
            island.ohsi.DisplayFinding();

            //island.nextFinding = explodedHole;
            //island.ShowNextFinding(explodedHole);
            //island.map[island.nextFindingIndex] = explodedHole;
        }
    }

    public OnHoverShowInfo ohsi;

    public void RemoveIslands()
    {
        foreach (Transform islandTransform in islandFolder)
        {
            Destroy(islandTransform.gameObject, 5f);
        }
        islands = new List<Island>(0);
    }
    public void GenerateIslands(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            islands.Add(SpawnIsland());

        }
    }
    public Island SpawnIsland()
    {
        

        Vector3 position()
        {
            float frameWidth = 0.83f;

            Vector3 point = Vector3.zero;
            do
            {
                point = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width * frameWidth), Random.Range(Screen.height - Screen.height * frameWidth, Screen.height * frameWidth), 10/*Camera.main.farClipPlane / 2*/));
            } while (point.x < -6);
            // return new Vector3(Random.value * 30, Random.value * 20f - 10f) + islandFolder.transform.position + Vector3.right * exploreDistance;
            return point + Vector3.right * exploreDistance;
        }

        GameObject island = Instantiate(islandsToSpawn[Random.Range(0, islandsToSpawn.Length)], position(), Quaternion.Euler(0, 0, Random.value * 360), islandFolder);

        bool canSpawn = true;

        int count = 0;

        float minDistance = 8.65f;

        do
        {

            canSpawn = true;
            count++;
            island.transform.position = position();

            foreach (Island land in islands)
            {
                Vector3 distanceVector = land.transform.position - island.transform.position;
                float distance = distanceVector.magnitude;

                if (distance < minDistance /*|| island.transform.position.x < -9*/)
                {
                    canSpawn = false;
                }
            }

            if(count > 100)
            {
                count = 0;
                minDistance -= 0.05f;

                if(minDistance < 1f)
                {
                    break;
                }
            }

        } while (!canSpawn);

        return island.GetComponent<Island>();
    }

}

[CreateAssetMenu(fileName = "New Finding", menuName = "Findings", order = 1)]
public class Finding : ScriptableObject
{
    public new string name;

    public float score;
    public float damage;

    public Sprite sprite;

    public float poison;

    public AudioClip sound;

    public string funText;
    public string extra;
}

public class LastFinding
{
    public Island island;
    public int index;

    public LastFinding(Island island, int index)
    {
        this.island = island;
        this.index = index;
    }

}


public enum TaskType
{
    WOLF,
    VENUM,
    RADIOACTIVE,
    BONE
}

[CreateAssetMenu(fileName = "New Task", menuName = "Tasks", order = 1)]
public class Task : ScriptableObject
{
    public TaskType type;

    public string explanation;
    public GameObject symbol;

    public AudioClip sound;

    #region is changed dynamicly
    public int currentAmmount = 0;
    public int aimAmmount;
    public TextMeshProUGUI ammountText;
    #endregion

    // Returns if the objective is finished
    public bool CheckAdd(TaskType type, int add)
    {
        if(this.type == type)
        {
            currentAmmount += add;
        }

        if(currentAmmount >= aimAmmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

public class Objective
{
    public List<Task> tasks = new List<Task>();

    public Objective(List<Task> tasks)
    {
        this.tasks = tasks;
    }

    // Displays it for the first time
    public void DisplayObjective()
    {
        // Remove all taskprefabs in the objectivespace


        // Instantiate all taskprefabs in the objectivespace
        var startPosition = GameManager.instance.inventory.startPosision.position;
        var interval = GameManager.instance.inventory.startPosision.localScale;

        int i = 0;
        foreach (var task in tasks)
        {
            task.explanation.Replace("{0}", task.aimAmmount.ToString());
            if(task.aimAmmount >= 1)
                task.explanation.Replace("{s}", "s");
            else
                task.explanation.Replace("{s}", "");

            Vector3 offset = Vector3.zero;
            offset.x = GameManager.instance.inventory.startPosision.localScale.x * (i % 4);
            offset.y = GameManager.instance.inventory.startPosision.localScale.y * Mathf.Floor(i/4);

            TaskObject to = Object.Instantiate(GameManager.instance.taskObject.gameObject, GameManager.instance.inventory.startPosision.position + offset, Quaternion.identity, GameManager.instance.inventory.canvas).GetComponent<TaskObject>();            
            to.AddSprite(task);
            i++;
        }
    }

    // Updates ammounts and stuff
    public void Update(TaskType type, int add)
    {
        Debug.Log("Killed a wolf");
        foreach (var task in tasks)
        {
            task.CheckAdd(type, add);

            task.ammountText.text = task.currentAmmount.ToString();
        }
    }
}
