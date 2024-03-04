using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //public float startHealth = 1f;

    private Vector3 mousePosition;
    //public float movementSpeed = 0.1f;
    //public float health = 1f;
    //public float regen = 0.001f;

    //private SpriteRenderer sr;

    //void Start()
    //{
    //    sr = GetComponentInChildren<SpriteRenderer>();
    //}

    public void ResizeToSize(float newSize)
    {
        transform.localScale = Vector3.one * (newSize + 0.5f);
    }

    void LateUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.Lerp(transform.position, mousePosition, movementSpeed);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        //health += regen * Time.deltaTime;
        //UpdateColor();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

    //    if(obstacle == null)
    //        return;

    //    if (health <= obstacle.damage)
    //    {
    //        obstacle.damage -= health;
    //        obstacle.UpdateStuff();
    //        health = 0;
    //        UpdateColor();
    //        Die();
    //    }
    //    else
    //    {
    //        health -= obstacle.damage;
    //        UpdateColor();
    //        obstacle.damage = 0;
    //        obstacle.UpdateStuff();

    //    }
    //}

    //public void UpdateColor()
    //{
    //    float blueness = health;

    //    sr.color = new Color(128f/255, 149f/255, blueness);
    //}

    //public void Die()
    //{

    //    GameManager.instance.gameOver.SetActive(true);

    //}

    //public void Respawn()
    //{
    //    // Healing player
    //    health = startHealth;

    //    UpdateColor();
    //}

}