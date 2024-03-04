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


        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    public AudioSource audioSource;
    public AudioClip eliminationSound;

    public bool displayIdsOnSquares = true;   

    public float volume = 1.0F;

    public SquareSpawner squareSpawner;
    public Levels levels;

    public bool levelIsrevealed = false;
    public bool isAltered = false;

    public float timeSpent = 0.0F;
    public TextMeshProUGUI timeSpentText;

    public void SetTimeSpentTo(float time)
    {
        timeSpent = time;
        timeSpentText.text = time.ToString();
        if (timeSpent <= 0)
        {
            timeSpentText.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            displayIdsOnSquares = !displayIdsOnSquares;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetTimeSpentTo(0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            squareSpawner.EliminateRandomSquare();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (levelIsrevealed)
            {
                levels.NextLevel();
                levelIsrevealed = false;
                isAltered = false;
            }
            else
            {
                if(!isAltered)
                {
                    float revealSpeed = 1.0F;
                    isAltered = true;
                    StartCoroutine(RevealEverySecond());

                    IEnumerator RevealEverySecond()
                    {
                        while (!levelIsrevealed)
                        {
                            squareSpawner.EliminateRandomSquare();
                            SetTimeSpentTo(timeSpent + revealSpeed);
                            yield return new WaitForSeconds(revealSpeed);
                        }

                    }
                }
                else
                {
                    squareSpawner.RevealAnswer();
                    levelIsrevealed = true;
                    isAltered = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (levelIsrevealed)
            {
                squareSpawner.ReplaceAllSquares();
                levelIsrevealed = false;
                isAltered = false;
            }
            else
            {
                if (isAltered)
                {
                    squareSpawner.ReplaceAllSquares();
                    levelIsrevealed = false;
                    isAltered = false;
                }
                else
                {
                    levels.PreviousLevel();
                    levelIsrevealed = false;
                    isAltered = false;
                }
            }
        }
    }

}
