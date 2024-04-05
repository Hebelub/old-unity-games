using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SquareSpawner : MonoBehaviour
{

    public Vector2Int gridSize;

    public GameObject squareObject;

    // public Vector2 spawnDistance = Vector2.one;

    public bool centralized = true;

    private void Start()
    {
        InstantiateSquares();
    }

    public void InstantiateSquares()
    {
        int currentId = 0;
        for (int y = gridSize.y - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject square = InstantiateSquareAt(x, y);

                if(GameManager.instance.displayIdsOnSquares)
                {
                    square.GetComponentInChildren<TextMeshProUGUI>().text = currentId.ToString();

                    currentId += 1;
                } 
            }
        }

        GameObject InstantiateSquareAt(int coord_x, int coord_y)
        {
            float pos_x = coord_x;
            float pos_y = coord_y;

            if (centralized)
            {
                pos_x -= (float) (gridSize.x - 1) / 2;
                pos_y -= (float) (gridSize.y - 1) / 2;
            }

            Vector3 instancePosition = new Vector3(pos_x, pos_y, 0);

            return Instantiate(squareObject, instancePosition, Quaternion.identity, transform);
        }
    }

    public void EliminateRandomSquare()
    {
        if (!GameManager.instance.isAltered)
        {
            GameManager.instance.isAltered = true;
        }

        if(transform.childCount > 0)
        {
            int childToEliminate = Random.Range(0, transform.childCount);

            transform.GetChild(childToEliminate).GetComponent<DestroyOnTouch>().Eliminate();
        }
        else
        {
            GameManager.instance.levelIsrevealed = true;
        }
    }

    public void ReplaceAllSquares()
    {
        DestroyAllSquares();
        InstantiateSquares();
    }

    public void RevealAnswer()
    {
        int childCount = transform.childCount;
        StartCoroutine(RevealAnswer());


        IEnumerator RevealAnswer()
        {
            GameManager.instance.volume = 0F;
            while(transform.childCount > 0)
            {
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.eliminationSound, 0.4F);

                for (int i = 0; i < 10; i++)
                {
                    EliminateRandomSquare();
                }
                yield return null;
            }
            GameManager.instance.volume = 1.0F;
        }


        //    foreach (Transform child in transform)
        //    {
        //        child.GetComponent<DestroyOnTouch>().Fade();
        //    }
    }

    public void DestroyAllSquares()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
