using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public Vector3Int gridSize = Vector3Int.one * 4;

    public Tile[,,] world = new Tile[4, 4, 1];

    // 2048 tiles array
    public GameObject[] tileGos;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChecMove(Vector3Int.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChecMove(Vector3Int.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChecMove(Vector3Int.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChecMove(Vector3Int.right);
        }

    }

    public void ChecMove(Vector3Int direction)
    {
        // Needs to check all tiles and see if tiles can move
        // If CheckMove(down) it should check the tiles wich is at the bottom first, because this can open up for the tiles behind
        // If a tile is set to move, move that tile via a method in the tile

        bool beginRight = false;
        bool beginDown = false;

        if (direction == Vector3Int.up)
        {
        }
        else if (direction == Vector3Int.left)
        {
            beginRight = true;
        }
        else if (direction == Vector3Int.down)
        {
            beginDown = true;
        }
        else if (direction == Vector3Int.right)
        {
        }

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                for (int k = 0; k < gridSize.z; k++)
                {
                    int x = i;
                    int y = j;
                    int z = k;
                    if (beginRight)
                    {
                        x = gridSize.x - x;
                    }
                    if (beginDown)
                    {
                        y = gridSize.y - y;                       
                    }
                    else{}

                    world[x, y, z].Move(direction);

                }
            }
        }

    }

}

public class Tile
{
    public float value;
    private int doubelingNumber;
    public GameObject gameObject;
    private GameManager gm = GameManager.instance;
    private Vector3Int atPosition;

    public Tile(float value)
    {
        this.value = value;   
    }

    public void DoubleTile()
    {
        value *= 2;
        doubelingNumber += 1;
    }

    private void FindCorespondingGameObject()
    {
        gameObject = gm.tileGos[doubelingNumber];
    }

    public void Move(Vector3Int direction)
    {
        
    }

}
