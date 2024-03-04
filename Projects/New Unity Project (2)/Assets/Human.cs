using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Alive
{
    public bool canReproduce = false;
    
    public float timeBetweenBirths = 20f;

    public float lifeTime = 200f;

    public GameObject baby;
    public GameObject bones;

    private void Start()
    {
        StartCoroutine(IAgeDeath());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();

        if(entity._name == "Human" && canReproduce == true)
        {
            canReproduce = false;

            collision.gameObject.GetComponent<Human>().canReproduce = false;

            //if(entity.id < GetComponent<Entity>().id)
                DropItem(baby, 2f);

            //StartCoroutine(StartReproduction());
            //entity.StartCoroutine(StartReproduction());
        }
        else if(entity._name == "Steaked Chicken"
            || entity._name == "Fish"
            || (entity._name == "Egg" && Random.value < 0.25f))
        {
            Destroy(entity.gameObject);

            canReproduce = true;
        }else if (entity._name == "Chicken Meat")
        {
            Destroy(entity.gameObject);
            Die();
        }

        //IEnumerator StartReproduction()
        //{
        //    yield return new WaitForSeconds(timeBetweenBirths * Random.value);

        //    canReproduce = true;
        //}

    }
    public IEnumerator IAgeDeath()
    {
        yield return new WaitForSeconds(lifeTime * (Random.value + 0.2f));

        Die();
       

    }

    public void Die()
    {
        DropItem(bones, 0f);

        Destroy(gameObject);
    }

}
