using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region Singelton
    public static LevelGenerator instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameObject obstacle;
    public GameObject goal;
    public GameObject portal;

    public int levelAtOnce = 16;

    public int level = 0;

    public List<GameObject> levelObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < levelAtOnce; i++)
        {
            CreateLevel();
            ActivateFirstLevel(true);
        }
    }

    Vector2 lastGoalPos = new Vector2(0, 0);
    public void CreateLevel()
    {
        int levelSize = level + 3;

        GameObject levelObject = new GameObject();
        levelObject.transform.position += (Vector3.right + Vector3.up) * -(levelSize / 2);

        Vector2 goalPos;
        do
        {
            goalPos = new Vector2(Random.Range(0, levelSize), Random.Range(0, levelSize));
        } while (lastGoalPos == goalPos);

        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                int a = Mathf.FloorToInt(level / 2);
                int x = i - a;
                int y = j - a;
                
                if(new Vector2(i, j) == goalPos)
                {
                    AddObstacle(new Vector3(x, y, level), goal, levelObject.transform);
                }
                else if(Random.value < 0.5f && new Vector2(i, j) != lastGoalPos)
                {
                    AddObstacle(new Vector3(x, y, level), obstacle, levelObject.transform); 
                }
                else if(Random.value < 0.01f)
                {
                    AddObstacle(new Vector3(x, y, level), portal, levelObject.transform);
                }
            }
        }

        levelObjects.Add(levelObject);

        lastGoalPos = goalPos;
        level++;
    }

    public void AddObstacle(Vector3 position, GameObject gameObject, Transform levelObject)
    {
        Instantiate(gameObject, position, Quaternion.identity, levelObject);
    }

    public void ActivateFirstLevel(bool activate)
    {
        for (int i = 0; i < levelObjects[0].transform.childCount; i++)
        {
            GameObject c = levelObjects[0].transform.GetChild(i).gameObject;
            c.GetComponent<Block>().ActivateColor(activate);
        }
    }

}
