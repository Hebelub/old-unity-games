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

    public PlayerMovement player;

    public List<MovingRelativeToPlayer> creatures = new List<MovingRelativeToPlayer>();

    public GameObject emptyCircle;

    private void Start()
    {
        List<Vector3> possiblePoints = FindPossiblePoints();

        foreach (Vector3 possible in possiblePoints)
        {
            Instantiate(emptyCircle, possible, Quaternion.identity);
        }
    }

    public List<Vector3> FindPossiblePoints()
    {
        List<Vector3> possiblePoints = new List<Vector3>();

        for(int x = 0; x < 200; x++)
        {
            for(int y = 0; y < 200; y++)
            {
                Vector3 testPos = new Vector3(x * 0.05F - 5, y * 0.05F - 5, player.transform.position.z);

                if(player.IsLeagalPosition(testPos))
                {
                    possiblePoints.Add(testPos);
                }
            }
        }

        return possiblePoints;
    }

}
