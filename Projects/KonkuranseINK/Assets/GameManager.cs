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

    public Transform pointSpawner;
    public GameObject pointAdders;

    public List<Team> allPoints = new List<Team>();

    public bool isCheckingScore = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isCheckingScore)
            {
                isCheckingScore = true;
                CheckScore();
            }
            else
            {
                isCheckingScore = false;
                DeCheckScore();
            }
        }
    }

    public void SpawnPoint(Team team)
    {
        Vector3 offset = Random.insideUnitSphere * 4;
        GameObject go = Instantiate(team.gameObject, pointSpawner.position + offset, Quaternion.identity);
        go.transform.rotation = new Quaternion(Random.value, Random.value, Random.value, 0);

        go.GetComponent<Rigidbody>().AddForce((Random.insideUnitSphere + Vector3.up) * 50f);
        go.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 50f);
    }

    public Transform scorepointThing;

    float cameraChanging = 1.7F;
    public void DeCheckScore()
    {
        Camera.main.fieldOfView /= cameraChanging;

        pointAdders.SetActive(true);
        bowl.SetActive(true);
        // ground.SetActive(true);

        foreach (Team point in allPoints)
        {
            if (point == null) continue;

            point.GetComponent<Rigidbody>().isKinematic = false;

            point.transform.position = point.positionWhenMoved;
        }
    }
    public void CheckScore()
    {
        Camera.main.fieldOfView *= cameraChanging;

        pointAdders.SetActive(false);
        bowl.SetActive(false);
        // ground.SetActive(false);

        int[] pointGrid = new int[20];

        foreach (Team point in allPoints)
        {
            if (point == null) continue;

            point.positionWhenMoved = point.transform.position;

            point.GetComponent<Rigidbody>().isKinematic = true;

            float forwards = pointGrid[point.playerTeam] * 2.0F % 20;
            float width = point.playerTeam * 4.0F;
            float up = 2 * Mathf.Floor(pointGrid[point.playerTeam] * 2.0F / 20);

            int num = 16;
            if(up >= num)
            {
                up -= num;
                width += 2;

                //if (up >= num)
                //{
                //    forwards = Random.value * 30f;
                //    width = Random.value * 30f;
                //    up = Random.value * 30f;
                //}
            }

            pointGrid[point.playerTeam] += 1;

            point.transform.position = new Vector3(width, up, forwards) + scorepointThing.position;
        }
    }

    public GameObject bowl;
    public GameObject ground;

}
