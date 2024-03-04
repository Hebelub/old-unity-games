using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject TestGameObjectForNow;

    public Vector2Int gridSize;
    public float tileSize;

    public World world;

    public Transform origon;

    private void Start()
    {
        //spawnworld(generateworld());
        GenerateWorld();
    }

    public World GenerateWorld()
    {
        return new World(gridSize);
    }
    //public void SpawnWorld(World world)
    //{
    //    for (int i = 0; i < gridSize.x; i++)
    //    {
    //        for (int j = 0; j < gridSize.y; j++)
    //        {
    //            Instantiate(world.tiles[i, j].gameObject, origon.position + new Vector3(i * tileSize, j * tileSize), Quaternion.identity, origon);
    //            world.tiles[i, j].gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

    //        }
    //    }
    //}
    public GameObject SpawnThis(Tile tile)
    {
        return Instantiate(tile.gameObject, origon.position + tile.atPosition, Quaternion.identity, origon);
    }
}

public class World
{
    public Tile[,] tiles;

    public World(Vector2Int gridSize)
    {
        tiles = new Tile[gridSize.x, gridSize.y];

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                tiles[i, j] = new Tile(new Vector3Int(i, j, 0));
            }
        }
    }
}

public class Tile
{
    public GameObject gameObject;
    public float hardness;
    public Drop drop;
    public Color color;
    public Vector3 atPosition;

    private GameManager gm = GameManager.instance;

    public Tile(Vector3Int coord)
    {
        atPosition = (Vector3)coord * gm.tileSize;
        color = new Color(Random.value, Random.value, Random.value);
        gameObject = gm.TestGameObjectForNow;
        gameObject = gm.SpawnThis(this);
        gameObject.GetComponent<SpriteRenderer>().color = color;
        hardness = 1f;
        drop = new Drop();
    }

}
public class Drop
{
    List<Item> items;

    public Drop()
    {

    }
}
public class Item
{

}
