using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float helium = 1f;
    public float fillSpeed = 0.05F;
    public float emptySpeed = 0.01F;

    public AudioClip[] inhaleA;
    public AudioClip[] exhaleA;
    public AudioClip[] happyA;
    public AudioClip[] sadA;
    public AudioClip[] idleA;
        public AudioClip[] windA; // Maby this should be on the wind script

    public AudioSource aS;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                RandomSound(inhaleA);
            }
            helium += fillSpeed;
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                RandomSound(exhaleA);
            }
            helium -= emptySpeed;
            if (helium < 0)
            {
                helium = 0;
            }
            if (Random.value < 0.01f)
            {
                RandomSound(idleA);
            }
        }

        Scale();
        Move();

        void Scale()
        {
            transform.GetChild(0).localScale = helium * Vector3.one;
        }
        void Move()
        {
            transform.Translate(Vector3.up * ((helium - 2f) / 25f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Sharp"))
        {
            Pop();
        }
    }

    public void Pop()
    {
        RandomSound(sadA);
    }

    public void RandomSound(AudioClip[] possible)
    {
        aS.PlayOneShot(possible[Random.Range(0, possible.Length)]);   
    }

}
