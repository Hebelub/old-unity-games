using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    private InputField powerField;

    private GameManager gm;

    public bool isAI;

    private Tile tile;

    private float moveSpeed = 5.4f;

    public string userName;

    public float score;
    public int kills;
    public int moves;

    private float AIdifficulty = 0.2f; // How much time between each thinking interval

    public Vector3Int coord;

    public bool isAlive = false;

    private Vector3Int direction;

    public List<Power> powers;

    public GameObject cameraPrefab;
    public GameObject powerFieldPrefab;

    private void Start()
    {
        if(!hasAuthority)
        {
            return;
        }

        powerField = powerFieldPrefab.transform.GetChild(0).GetComponent<InputField>();

        gm = GameManager.instance;

        Setup();

    }

    private void Setup()
    {
        SetupCamera();
        SetupPowerField();

        Respawn();
    }
    private void SetupCamera()
    {
        // The camera should only be instatiated for this player
        GameObject go = Instantiate(cameraPrefab, transform.position + cameraPrefab.transform.position, Quaternion.identity);

        go.GetComponent<CameraFollow>().lookAt = transform;

    }
    private void SetupPowerField()
    {
        // The setupField should only be instantiated for this player
        Instantiate(powerFieldPrefab);

    }

    private IEnumerator PlayerControlls()
    {
        while (isAlive)
        {
            if (Input.anyKeyDown)
            {
                if (CheckPosition(Vector3Int.up))
                {
                    PlayerMoved();
                }
                else if (CheckPosition(Vector3Int.left))
                {
                    PlayerMoved();
                }
                else if (CheckPosition(Vector3Int.down))
                {
                    PlayerMoved();
                }
                else if (CheckPosition(Vector3Int.right))
                {
                    PlayerMoved();
                }
                else
                {
                    MissClick();
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    if(!powerField.isFocused)
                    {
                        powerField.Select();

                    }
                    powerField.DeactivateInputField();

                    powerField.text = "";
                }

            }
            
            yield return null;
        }
    }
    private void PlayerMoved()
    {
        ChangeScore(1f);
        moves += 1;
    }
    private void MissClick()
    {
        ChangeScore(-1f);
    }
    private void ChangeScore(float change)
    {
        score += change;
        if (score < 0)
        {
            score = 0;
        }
    }
    private IEnumerator PlayerMovement()
    {
        while(isAlive)
        {

            transform.position = Vector3.MoveTowards(transform.position, gm.CoordToPos(coord), moveSpeed * Time.deltaTime);

            yield return null;
        }
    }
    private IEnumerator AIMovement()
    {
        while(isAlive)
        {
            // Moves in a random direction but it should definitly work compleatly differently and be more humanlike
            if(Random.Range(0, 4) == 0)
            {
                // Move
                int direction = Random.Range(0, 4);

                if (direction == 0)
                {
                    if (AICheckPosition(Vector3Int.up))
                        PlayerMoved();
                }
                else if (direction == 1)
                {
                    if (AICheckPosition(Vector3Int.left))
                        PlayerMoved();
                }
                else if (direction == 2)
                {
                    if (AICheckPosition(Vector3Int.down))
                        PlayerMoved();
                }
                else if (direction == 3)
                {
                    if (AICheckPosition(Vector3Int.right))
                        PlayerMoved();
                }
            }

            yield return new WaitForSeconds(AIdifficulty);
        }
    }
    
    public bool CheckPosition(Vector3Int relativeCoord)
    {
        Tile newTile = gm.GetTile(coord + relativeCoord);

        if (newTile != null && newTile.player == null && Input.GetKeyDown(newTile.letter))
        {
            tile.PlayerExit();
            newTile.PlayerEnter(this);
            coord += relativeCoord;
            tile = newTile;
            Face(relativeCoord);
            return true;
        }
        return false;
    }
    private bool AICheckPosition(Vector3Int relativeCoord)
    {
        Tile newTile = gm.GetTile(coord + relativeCoord);

        if (newTile != null)
        {
            tile.PlayerExit();
            newTile.PlayerEnter(this);
            coord += relativeCoord;
            tile = newTile;
            Face(relativeCoord);
            return true;
        }
        return false;
    }

    private void Face(Vector3Int newDirection)
    {
        if (newDirection != direction)
        {
            if (newDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (newDirection.x > 0)
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    public void Die(Player killedBy)
    {
        // Debug.Log(userName + " was killed by " + killedBy.userName);

        isAlive = false;
        StopAllCoroutines();

        killedBy.kills += 1;
        killedBy.score += (int)(score / 10);
        score = 0f;

        gameObject.SetActive(false); // Should show death animation
    }
    public void Respawn()
    {
        isAlive = true;
        gameObject.SetActive(true);

        int x = Random.Range(0, gm.grid.x);
        int y = Random.Range(0, gm.grid.y);
        int z = Random.Range(0, gm.grid.z);

        coord = new Vector3Int(x, y, z);

        transform.position = gm.CoordToPos(coord);

        tile = gm.GetTile(coord);

        if (!isAI)
        {
            StartCoroutine(PlayerControlls());
        }
        else
        {
            StartCoroutine(AIMovement());
        }
        StartCoroutine(PlayerMovement());

    }

    public void CheckActivation(string activation)
    {
        switch (activation)
        {
            case "hit":
                Hit();
                break;
            case "point":
                Score();
                break;
            case "bazooka":
                Bazooka();
                break;
            case "sniper":
                Sniper();
                break;
            case "granate":
                Debug.Log("Granate");
                break;
            case "teleport":
                Debug.Log("Rendomly teleported!");
                break;

                // Earthquake
                    // Remakes all tiles in a radius
                // Protection
                    // 
                // Freeze
                    // Makes nebary players unable to move or do anything for a period of time
                // Invisibility
                    // Makes you invisible for a certain ammount of moves or time
                // Escape
                    // Your player runs away from the closest players
        }

    }

    private void Hit()
    {
        // Kills all adjacent players
        Debug.Log("Hit");

    }
    private void Score()
    {
        // Gives a point to the player
        ChangeScore(1f);
    }
    private void Bazooka()
    {

    }
    private void Sniper()
    {
        // Shoots in a long range
    }
    
}
