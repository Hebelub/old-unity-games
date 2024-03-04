using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public AudioSource audioSource;

    public List<Player> players = new List<Player>();

    public int maxSeconds;
    public int minSeconds;

    public float chanceEverySecond;

    public int currentSecond = 0;

    public AudioClip explotionSound;

    public TextMeshProUGUI tet;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(IExplotion());
        tet.text = "No player has exploded yet";
        UpdateText();

    }

    public bool isBombToggled = true;
    public Toggle bombToggle;
    public void ActivateBomb()
    {
        isBombToggled = bombToggle.interactable;

        if(bombToggle.isOn == true)
        {
            StartCoroutine(IExplotion());
        }
        else
        {

        }
    }

    public IEnumerator IExplotion()
    {
        while (isBombToggled)
        {
            yield return new WaitForSeconds(1f);

            currentSecond++;

            if ((Random.value < chanceEverySecond && currentSecond >= minSeconds) || currentSecond >= maxSeconds)
                Explode();
        }
    }

    public Camera camera;

    public void Explode()
    {
        explodedPlayer = playersTurn;

        currentSecond = 0;

        audioSource.PlayOneShot(explotionSound, 1f);

        UpdateText();

        SwitchPlayer();

    }

    public void UpdateText()
    {
        tet.text = "Last Exploded:\n" + explodedPlayer.ToString();
        text.text = "Players Turn:\n" + playersTurn.ToString();



    }



    public int nrOfPlayers = 2;
    public int playersTurn;
    public int explodedPlayer = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPlayer();
        }
    }

    public TextMeshProUGUI text;

    public void SwitchPlayer()
    {
        playersTurn += 1;
        if (playersTurn > nrOfPlayers)
        {
            playersTurn -= nrOfPlayers;
        }

        if (playersTurn == 1)
        {
            camera.backgroundColor = Color.white;
          //  chanceEverySecond /= 100f;
        }
        else
        {
            camera.backgroundColor = Color.black;
          //  chanceEverySecond *= 100f;
        }

        UpdateText();

    }


}
