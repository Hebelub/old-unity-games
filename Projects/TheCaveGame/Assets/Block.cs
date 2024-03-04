using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool canFall = false;

    public bool isMoving = false;

    public Vector3 wantedPosition;
    private void Start()
    {
        wantedPosition = transform.position;
    }

    public string actions;

    public Vector3Int lastMove;

    private Vector3 from;
    private Vector3 pushDirection;
    private Block pusher;
    private Vector3 lastPosition;
    public void OnLeaveUnder(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

        CheckFall();
    }
    public void OnLeaveSides(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

    }
    public void OnUnder(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

    }
    public void OnTouch(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

    }
    public void OnStep(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

        switch (actions)
        {
            case "lift":
                Rise();
                break;
            default:
                break;
        }
    }
    public void OnStepLeave(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

        switch (actions)
        {
            case "shot":
                Rise();
                break;
            default:
                break;
        }

    }
    public void OnPush(Vector3 from, Vector3 pushDirection, Block pusher)
    {
        this.from = from;
        this.pushDirection = pushDirection;
        this.pusher = pusher;
        lastPosition = transform.position;

        switch (actions)
        {
            case "player":
                Light();
                PusherHere();
                break;
            case "sand":
                Light();
                PusherHere();
                break;
            case "elevator":
                Lift();
                break;
            case "moveable elevator":
                Light();
                PusherHere();
                Lift();
                break;
            case "lift door":
                Rise();
                PusherHere();
                break;
            default:
                break;
        }

    }
    public void Rise()
    {
        Move(wantedPosition, transform.up);
    }

    public void Light()
    {
        Move(wantedPosition, pushDirection/*, this*/);
    }

    public void PusherHere()
    {
        pusher.Move(from, pushDirection/*, pusher*/);
    }

    public void Lift()
    {
        pusher.Move(wantedPosition, transform.up/*, pusher*/);

        //if(CanWalk(transform.position, Vector3.up, pusher))
        //{

        //    pusher.Teleport(transform.position + transform.up);
        //}
    }

    public void Teleport(Vector3 position)
    {
        if (transform.position != position)
        {
            transform.position = position;

            StartMove();
        }
    }

  //  public List<Vector3> tryesToMoveDirections = new List<Vector3>();
    public void Move(Vector3 from, Vector3 direction/*, Block mover*/)
    {
      //  tryesToMoveDirections.Add(direction);
        if (CanWalk(from, direction/*, mover*/))
        {
            //   /*mover.*/transform.position = from + direction;
            StartCoroutine(IMove());
            StartMove();
        }
   //     tryesToMoveDirections = new List<Vector3>();

        IEnumerator IMove()
        {
            wantedPosition = from + direction;

            isMoving = true;

            var t = 0.0f;
            while(t < 1)
            {
                t += Time.deltaTime * 4;

                transform.position = Vector3.MoveTowards(from, wantedPosition, t);

                yield return null;
            }

            transform.position = from + direction;

            isMoving = false;

            AfterMove(from);
        }

        //else if (CanWalk(Vector3.up) && CanWalk(direction + Vector3.up))
        //{
        //    M(direction + Vector3.up);
        //}
    }

    bool CanWalk(Vector3 from, Vector3 direction/*, Block mover*/)
    {
        //foreach (Vector3 aTry in tryesToMoveDirections)
        //{
        //    if (aTry == direction)
        //    {
        //        return false;
        //    }
        //}
        //tryesToMoveDirections.Add(direction);


        var lvl = GameManager.instance.level.transform;

        bool canWalk = true;

        Block block = GetBlockAt(from + direction);

        if(block != null)
        {
            canWalk = false;
            block.OnPush(from, direction, this);
        }
    
        return canWalk;
    }

    public void AfterMove(Vector3 wasAt)
    {
        CheckFall();

        foreach (Block b in GetTouching(wasAt))
        {
            b.OnLeaveSides(wantedPosition, wantedPosition - wasAt, this);
            if (b.transform.position.y > wasAt.y)
            {
                b.OnLeaveUnder(wasAt, wantedPosition - wasAt, this);
            }
            else if (b.transform.position.y < wasAt.y)
            {
                b.OnStepLeave(wasAt, wantedPosition - wasAt, this);
            }
        }
        foreach (Block b in GetTouching(wantedPosition))
        {
            b.OnTouch(wantedPosition, wantedPosition - wasAt, this);
            if (b.transform.position.y > wantedPosition.y)
            {
                b.OnUnder(wantedPosition, wantedPosition - wasAt, this);
            }
            else if(b.transform.position.y < wantedPosition.y)
            {
                b.OnStep(wantedPosition, wantedPosition - wasAt, this);
            }


        }

    }
    public void StartMove()
    {

    }

    public List<Block> GetTouching(Vector3 position)
    {
        List<Block> touching = new List<Block>();

        foreach(Transform t in GameManager.instance.level.transform)
        {
            Block b = t.GetComponentInParent<Block>();

            if(!b.isMoving && 1.1f > Vector3.Distance(position, b.wantedPosition))
            {
                touching.Add(b);
            }
        }

        return touching;
    }

    public bool CheckFall()
    {
        if (transform.position.y < -10)
        {
            canFall = false;
        }

        if (canFall)
        {
            if (GetBlockAt(wantedPosition + Vector3.down) == null)
            {
                Move(transform.position, Vector3.down);
                return true;
            }
        }
        return false;
    }

    Block GetBlockAt(Vector3 position)
    {
        var lvl = GameManager.instance.level.transform;

        foreach (Transform t in lvl)
        {
            Block b = t.GetComponentInParent<Block>();
            if (Vector3.Distance(position, b.wantedPosition) < 0.5f)
            {
                return b;
            }
        }

        return null;
    }

}
