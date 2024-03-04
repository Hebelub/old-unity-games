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

    public Ball ball;

    public Transform ballsFolder;

    public List<Player> players;

    public Text[] teamsText;
    public float[] teamsScore;

    private void Start()
    {
        teamsScore = new float[teamsText.Length];
        Score(0, 0);
        Score(1, 0);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Score(int team, float ammount)
    {
        teamsScore[team] += ammount;
        teamsText[team].text = teamsScore[team].ToString();
    }

    public void Restart()
    {
        ball.Detach();
        ball.transform.position = Vector3.zero;
        ball.rb.velocity = Vector3.zero;
        ball.rb.bodyType = RigidbodyType2D.Dynamic;

        foreach(Player player in players)
        {
            player.Restart();
        }

    }

}
