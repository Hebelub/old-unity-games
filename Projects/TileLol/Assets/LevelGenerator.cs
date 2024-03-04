using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tileGO;

    public int scoreI = 0;
    public TextMeshProUGUI scoreT;

    public List<Tile> allTiles;

    public AudioSource aS;
    public AudioClip playOnChange;
    public AudioClip burnSound;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        GenerateLevel();
    }

    public void GameOver()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            int tall = 0;
            foreach (Tile tile in allTiles)
            {
                if (tile.typeI == 0)
                {
                    tall++;
                    if (Random.value < 1.0F / 4.0F)
                    {
                        tile.willWalk = false;
                    }
                    else
                    {
                        tile.willWalk = true;
                    }
                }
            }
            if(tall <= 0)
            {
                GameOver();
            }
            else if(tall <= 1)
            {
                foreach (Tile tile in allTiles)
                {
                    if (tile.typeI == 0)
                    {
                        tile.willWalk = true;
                    }
                }
            }
            bool dropABomb = false;
            if(Random.value < 0.1F)
            {
                dropABomb = true;
            }
            if(dropABomb)
            {
                tall = Random.Range(0, tall) + 1;
            }
            int tall2 = 0;

            foreach (Tile tile in allTiles)
            {
                if(tile.typeI == 0)
                {
                    tall2++;
                }
                if(tall2 == tall && dropABomb &&  tile.typeI == 0)
                {
                    Instantiate(tile.lavaGO, tile.transform.position, Quaternion.identity);
                    tile.willWalk = true;
                }

                if(tile.typeI == 0)
                {
                    tile.Move();
                }
            }

            bool somethingBurn = false;
            foreach (Tile tile in allTiles.ToArray())
            {
                if(tile.InLava() != null)
                {
                    somethingBurn = true;
                    allTiles.Remove(tile);
                    Destroy(tile.gameObject);
                }
            }
            if (somethingBurn)
            {
                aS.PlayOneShot(burnSound);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            bool playSound = false;
            foreach (Tile tile in allTiles)
            {
                tile.Check();
            }
            foreach (Tile tile in allTiles)
            {
                if (tile.is3InARow)
                {
                    tile.Change();
                    playSound = true;
                    tile.is3InARow = false;
                    scoreI += 1;
                }
            }
            scoreT.text = scoreI.ToString();
            if(playSound) aS.PlayOneShot(playOnChange);
        }
    }

    public void GenerateLevel()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                PlaceTile(x - 3, y - 3);
            }
        }

        void PlaceTile(int x, int y)
        {
            GameObject go = Instantiate(tileGO, new Vector3(x, y, 0), Quaternion.identity);
            allTiles.Add(go.GetComponent<Tile>());
        }
    }

}
