using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public Vector3 grid; // Where the monster can move

    // public float squareSize = 1f;

    public float speed = 4f;

    public Vector3 wantedPosition;

    public Vector3 coordinates;

    //public float monsterFrameMovement = 1f;

    public float movementEveryFrame = 5f;

    private void Start()
    {
        StartCoroutine(MoveCoroutine());
        // StartCoroutine(MoveMonsterCoroutine());

        grid = GameManager.instance.grid - Vector3.one;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, movementEveryFrame);
        
        if (Input.GetMouseButtonDown(0))
        {
            Restart();
        }

    }

    public void Restart()
    {
        wantedPosition = Vector3.zero;
    }

    public IEnumerator MoveCoroutine()
    {
        bool monsterMoves = false;

        while (true)
        {
            if (Random.Range(0, 3) == 0)
            {
                monsterMoves = true;
            }

            if (monsterMoves)
            {
                RandomMove();
            }

            yield return new WaitForSeconds(speed);
        }
    }

    public void RandomMove()
    {
        int random = Random.Range(0, 4);

        Vector3 returner = Vector3.zero;

        if (random == 0 && coordinates.y < grid.y)
        {
            returner = Vector3.up;
        } else if (random == 1 && coordinates.x < grid.x)
        {
            returner = Vector3.right;
        } else if (random == 2 && coordinates.y > 0)
        {
            returner = Vector3.down;
        } else if (random == 3 && coordinates.x > 0)
        {
            returner = Vector3.left;
        }

        coordinates += returner;

        wantedPosition = coordinates * GameManager.instance.squareSize;

    }

    //public IEnumerator MoveMonsterCoroutine()
    //{
    //    while (true)
    //    {
    //        Vector3 currentPosition = transform.position;

    //        Vector3 movement = wantedPosition - currentPosition;

    //        Vector3 movementEveryFrame = movement / monsterFrameMovement;

    //        int i = 1;
    //        while (i < monsterFrameMovement)
    //        {
    //            i++;
    //            transform.position += movementEveryFrame;
    //            yield return null;
    //            Debug.Log(movementEveryFrame);
    //        }
    //        Debug.Log("A");
    //        transform.position = wantedPosition;

    //        yield return null;

    //    }
    //}

}
