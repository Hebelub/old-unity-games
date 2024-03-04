using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool willWalk = false;

    public int typeI = 0;
    public List<Sprite> typeS;

    private Vector3Int vU = Vector3Int.up;
    private Vector3Int vL = Vector3Int.left;
    private Vector3Int vR = Vector3Int.right;
    private Vector3Int vD = Vector3Int.down;

    public GameObject lavaGO;

    public SpriteRenderer sR;

    private void Start()
    {
        Change();
    }

    public void Change()
    {
        typeI = Random.Range(0, typeS.Count);

        sR.sprite = typeS[typeI];
    }

    public void Move()
    {
        if(typeI == 0 && willWalk)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                Direction(vU);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Direction(vL);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Direction(vR);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Direction(vD);
            }
        }
    }
    public void Direction(Vector3Int d)
    {
        var tile = Get(d);
        
        if(tile != null && !tile.willWalk)
        {
            tile.Direction(d);
        }

        transform.position += d;
    }

    public bool is3InARow = false;
    public void Check()
    {
        var u = Get(vU);
        var l = Get(vL);
        var r = Get(vR);
        var d = Get(vD);

        var ver = 1;
        var hor = 1;

        if(u != null && u.typeI == typeI)
        {
            ver++;

            var u2 = Get(vU * 2);
            if(u2 != null && u2.typeI == typeI)
            {
                ver++;
            }
        }
        if (d != null && d.typeI == typeI)
        {
            ver++;

            var d2 = Get(vD * 2);
            if (d2 != null && d2.typeI == typeI)
            {
                ver++;
            }
        }
        if (l != null && l.typeI == typeI)
        {
            hor++;

            var l2 = Get(vL * 2);
            if (l2 != null && l2.typeI == typeI)
            {
                hor++;
            }
        }
        if (r != null && r.typeI == typeI)
        {
            hor++;

            var r2 = Get(vR * 2);
            if (r2 != null && r2.typeI == typeI)
            {
                hor++;
            }
        }

        if(ver >= 3 || hor >= 3)
        {
            is3InARow = true;
        }
    }
    public Tile Get(Vector3Int d)
    {
        Collider2D col = Physics2D.OverlapBox(transform.position + d, Vector2.one * 0.5f, 0f);
        if (col != null)
        {
            return col.GetComponent<Tile>();
        }
        return null;
    }
    public Lava InLava()
    {
        Collider2D lava = Physics2D.OverlapBox(transform.position, Vector2.one * 0.5f, 0f);
        if(lava != null)
        {
            return lava.GetComponent<Lava>();
        }
        return null;
    }
}
