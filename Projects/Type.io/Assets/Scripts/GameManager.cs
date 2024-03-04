using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    public static GameManager instance = null;

    // All the letters that a tile can contain
    string possibleLetters = "abcdefghijklmnopqrstuvwxyz0123456789";

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private Transform letterTiles;

    public GameObject[] letterGos;

    public float tileSize = 1f;

    public Vector3Int grid;

    public Tile[,,] tiles;

    public Tile GetTile(Vector3Int coord)
    {
        if (coord.x < 0 || coord.y < 0 || coord.z < 0 || coord.x >= grid.x || coord.y >= grid.y || coord.z > coord.z)
        {
            return null;
        }
        return tiles[coord.x, coord.y, coord.z];
    }
    public void ListTile(Vector3Int coord, Tile what)
    {
        tiles[coord.x, coord.y, coord.z] = what;
    }

    public Vector3 CoordToPos(Vector3Int coords)
    {
        return (Vector3)coords * tileSize;
    }

    private void Start()
    {
        tiles = new Tile[grid.x, grid.y, grid.z];
        CreateWorld();
    }

    void Update()
    {

    }

    public void CreateWorld()
    {
        for (int i = 0; i < grid.x; i++)
        {
            for (int j = 0; j < grid.y; j++)
            {
                for (int k = 0; k < grid.z; k++)
                {
                    CmdReplaceLetterAt(new Vector3Int(i, j, k));
                }
            }
        }
    }

    [Command]
    public void CmdReplaceLetterAt(Vector3Int atCoord)
    {
        // The letter to press
        string letterForTile = "";
        // All the positions that the loop has to check for
        Vector3Int[] checkerOffsets = { new Vector3Int(0, 2, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -2, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-2, 0, 0), new Vector3Int(-1, 1, 0) };
        // If it is a valid letter on that position
        bool satisfyed = false;
        // The index for the random letter
        int random = 0;
        // Checing if the letter is valid, if not try again
        while (!satisfyed)
        {
            satisfyed = true;

            random = Random.Range(0, 36);
            letterForTile = GetKeyCode(random);

            Tile compareToTile = null;

            // All the positions it is checking
            for (int i = 0; i < checkerOffsets.Length; i++)
            {
                compareToTile = GetTile(checkerOffsets[i] + atCoord);

                if (compareToTile != null && compareToTile.letter == letterForTile)
                {
                    satisfyed = false;
                    break;
                }
            }

        }

        // If tile at that position exists, it will destroy the gameobject at that position
        if (GetTile(atCoord) != null)
        {
            NetworkServer.Destroy(GetTile(atCoord).gameObject);
        }

        GameObject go = Instantiate(letterGos[random], CoordToPos(atCoord), Quaternion.identity, letterTiles);

        // tile.gameObject = go;

        // Is creating an instance of the new tile
        Tile tile = new Tile(go, letterForTile, atCoord);
        // Is putting the new tile in the gridList
        ListTile(atCoord, tile);

        // Is spawning the the newly instantiated go on all clients
        NetworkServer.Spawn(go);
        
    }
    private string GetKeyCode(int number)
    {
        return possibleLetters[number].ToString();
    }

}
