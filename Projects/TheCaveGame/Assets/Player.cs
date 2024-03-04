using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Block
{
    // Controlls
    public KeyCode walkForward = KeyCode.UpArrow;
    public KeyCode walkLeft = KeyCode.LeftArrow;
    public KeyCode walkBack = KeyCode.DownArrow;
    public KeyCode walkRight = KeyCode.RightArrow;

    public KeyCode camTurnRight = KeyCode.A;
    public KeyCode camTurnLeft = KeyCode.D;
       
    void Update()
    {
        var p = transform.position;
        if (Input.GetKey(walkForward) && !isMoving)
        {
            Move(p, transform.forward/*, this*/);
            //Fall();
        }
        if (Input.GetKey(walkLeft) && !isMoving)
        {
            Move(p, -transform.right/*, this*/);
            //Fall();
        }
        if (Input.GetKey(walkBack) && !isMoving)
        {
            Move(p, -transform.forward/*, this*/);
            //Fall();
        }
        if (Input.GetKey(walkRight) && !isMoving)
        {
            Move(p, transform.right/*, this*/);
            //Fall();
        }

        if (Input.GetKeyDown(camTurnRight))
        {
            transform.Rotate(Vector3.up * -90);
        }
        if (Input.GetKeyDown(camTurnLeft))
        {
            transform.Rotate(Vector3.up * 90);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Vector3.zero;
        }

  //      CheckFall();

        void Fall()
        {
            var somethingFell = false;

            foreach (Transform t in GameManager.instance.level.transform)
            {
                Block b = t.GetComponentInParent<Block>();

                if (b.CheckFall()) somethingFell = true;
            }

            if (somethingFell) Fall();
        }

    }

    //public void LookingAt()
    //{
    //    RaycastHit hit = new RaycastHit();
    //    Ray ray = new Ray(head.transform.position, head.transform.TransformDirection(Vector3.forward));
    //    Physics.Raycast(ray, out hit, 4f, 1 << 9);

    //    if(hit.collider != null)
    //    {
    //        if(hit.collider.gameObject != focus)
    //        {
    //            GetComponent<Digger>().isDigging = false;
    //            GameManager.instance.selectBlockObject.SetActive(true);
    //            GameManager.instance.selectBlockObject.transform.position = hit.collider.transform.position;
    //            focus = hit.collider.gameObject;
    //        }
    //    }
    //    else
    //    {
    //        focus = null;
    //        GameManager.instance.selectBlockObject.SetActive(false);
    //    }

    //}

    //public void Digg()
    //{
    //    if (focus != null)
    //    {
    //        Block block = focus.GetComponentInParent<Block>();
    //        if (block != null)
    //        {
    //            GetComponent<Digger>().Digg(block);
    //        }

    //    }
    //}

}
