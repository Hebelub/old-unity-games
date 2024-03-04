using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector3 target;

    public bool resizeOnMovement = true;

    void Start()
    {
        target = transform.position;
    }

    public bool willResize = true;
    public bool randomizeSeed = true;

    public bool hasCrashed = false;

    public float scale = 20F;

    public float offsetX3 = 0F;
    public float offsetY3 = 0F;
    public void RandomizeSeed()
    {
        if(randomizeSeed)
        {
            offsetX3 = Random.Range(0f, 99999F);
            offsetY3 = Random.Range(0f, 99999F);
        }
    }

    public float PlayerPosToScale(Vector3 player)
    {
        if (willResize)
        {
            float size = Mathf.PerlinNoise(player.x / scale + offsetX3, player.y / scale + offsetX3) + 0.1F;

            return size;
        }
        return 1F;
    }

    void Update()
    {
        UpdateTarget();

        while (!IsAtTarget())
        {
            Vector3 newPosition;

            if(!hasCrashed)
            {
                newPosition = CloserToTarget(speed);
            }
            else
            {
                newPosition = CloserToTarget(speed);
                hasCrashed = false;
            }

            if(IsLeagalPosition(newPosition))
            {
                transform.position = newPosition;
                transform.localScale = Vector3.one * PlayerPosToScale(newPosition);
            }
            else
            {
                hasCrashed = true;
                break;
            }
        }
    }

    public void UpdateTarget()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
    }

    public bool IsAtTarget()
    {
        return transform.position == target;
    }

    public Vector3 CloserToTarget(float speed)
    {
        return Vector3.MoveTowards(transform.position, target, speed);
    }

    public bool IsLeagalPosition(Vector3 newPosition)
    {
        if(Vector3.Distance(newPosition, Vector3.zero) > (8 /* Size of arena*/ / 2) - transform.localScale.x / 2)
        {
            return false;
        }

        foreach(MovingRelativeToPlayer creature in GameManager.instance.creatures)
        {
            Vector3 cPos = creature.PlayerPosToPos(newPosition);
            float cScale = creature.PlayerPosToScale(newPosition);

            if(DoesCirclesCollide(newPosition, PlayerPosToScale(newPosition), cPos, cScale))
            {
                return false;
            }
        }

        return true;
    }

    public bool DoesCirclesCollide(Vector3 posA, float scaleA, Vector3 posB, float scaleB)
    {
        float distance = Vector3.Distance(posA, posB);
        
        if(distance <= (scaleA + scaleB) / 2)
        {
            return true;
        }
        return false;
    }
}
