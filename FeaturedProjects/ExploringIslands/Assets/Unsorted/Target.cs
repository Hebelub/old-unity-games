using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private float life = 0.75f;

    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            transform.localScale = Vector3.one * life;
            life -= Time.deltaTime;
            if (life <= 0) Destroy(gameObject);
        }
        else
        {
            transform.localScale = Vector3.one * (Mathf.Abs(Mathf.Sin(Time.time*10))/2 + 0.5f);
        }
    }



}
