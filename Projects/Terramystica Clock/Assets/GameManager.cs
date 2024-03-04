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

    public int currentPlayersTurn = -1;
    public Camera cam;

    public float timeEveryTurn = 100f;

    public float timeLeft = 0f;
    public float lostPoints = 0f;

    public float newTimeEveryTurn = 20f;

    public TextMeshProUGUI timeLeftText;
    public TextMeshProUGUI turnNumberText;
    public TextMeshProUGUI playerNameText;

    public AudioSource audioSource;
    public AudioClip ac;

    public List<Player> players = new List<Player>();

    public bool gamePlaying = false;

    public GameObject showOnPause;

    public class Player
    {
        public Color color;
        public string name;
        public float timeLeft;
        public float extraTime;
        public int playedTurns = 0;

        public Player(string name, Color color, float startingTime, float extraTime)
        {
            this.color = color;
            this.name = name;
            timeLeft = startingTime;
            this.extraTime = extraTime;
        }

        public void Remove()
        {
            instance.players.Remove(this);
            instance.NextPlayer();
        }

        public void Tick()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            instance.timeLeftText.text = Mathf.RoundToInt(timeLeft).ToString();
        }

        public void YourTurn()
        {
            if (instance.gamePlaying)
                instance.done = true;
            else
                instance.done = false;
            if (playedTurns > 0 && instance.gamePlaying)
                timeLeft += extraTime;

            instance.timeLeftText.text = Mathf.RoundToInt(timeLeft).ToString();
            instance.turnNumberText.text = "Turn: " + playedTurns.ToString() + "        Bonus: " + extraTime.ToString();
            instance.cam.backgroundColor = color;
            instance.playerNameText.text = name;

            if (instance.gamePlaying)
                playedTurns += 1;
        }
    }

    private void Start()
    {
        Play(false);


        //players.Add(new Player("Gabriel", Color.green, 120, 20));
        //players.Add(new Player("Jo Kjetil", Color.red, 120, 20));
        //players.Add(new Player("Benjamin", Color.yellow, 120, 20));
        //players.Add(new Player("Agnes", Color.black, 120, 20));
        //players.Add(new Player("Halvor", Color.blue, 120, 20));

        players.Add(new Player(names[Random.Range(0, names.Length)], Color.blue, 120, 30));
        players.Add(new Player(names[Random.Range(0, names.Length)], Color.blue, 120, 30));
        players.Add(new Player(names[Random.Range(0, names.Length)], Color.blue, 120, 30));

        NextPlayer();
    }

    public Player player;
    public Player standardPlayer = new Player("Press space to start", Color.cyan, 300, 30);

    public bool done;

    public void Play(bool playing)
    {
        gamePlaying = playing;

        if (playing && !done)
            player.YourTurn();

        showOnPause.SetActive(!gamePlaying);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            player.Remove();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePlaying)
                Play(false);
            else if (!gamePlaying)
                Play(true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddPlayer();
        }
        if (players.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextPlayer();
            }
        }

        if (gamePlaying && player != null)
        {
            player.Tick();
        }
    }

    public string[] names = { "Monsteret", "Den beste","Vinneren","Trollet","Slemmingen","Finingen","Jøken","Motstanderen","Hateren","Kongen","Sjefen i huset","Liket",
        "Den motbydelige", "Frøken","Herr kungjøring","Fjellklatreren","Gullungen","Voksen","Selen","Dyret","Den fine","Den kloke","Den smarte","Krabben","Bamsen",
        "!%)#j#(F#4557","Spøkelset","Sammarbeidspartneren","Kaffeelskeren","Sjokoladeelskeren","Pianisten","Gitaristen","Begravelseskonsulenten","Krabaten", "Kalven",
        "Brøkdelen", "Dronningen", "Prinsessa", "Prinsen", "Leka", "Musa", "Flundra", "Katten", "Kolibrien", "Ingenting å skryte av", "Den storartede", "Den hjelpsome",
        "Den feite", "Den treige", "Den med så veldig langt navn", "Den forvirra", "Svindleren", "Den beundringsverdige", "Skrytepaven", "Løkspiseren", "En som er helt topp"};

    public void AddPlayer()
    {
        Play(false);
        players.Insert(currentPlayersTurn + 1, new Player(names[Random.Range(0, names.Length)], players[0].color, players[0].timeLeft, players[0].extraTime));
        NextPlayer();
    }

    public void NextPlayer()
    {
         
        currentPlayersTurn++;

        if (currentPlayersTurn >= players.Count)
        {
            currentPlayersTurn -= players.Count;
        }

        currentPlayersTurn %= players.Count;

        player = players[currentPlayersTurn];
        player.YourTurn();

        if(player.playedTurns > 0)
        {
            Play(true);
        }
    }

}
