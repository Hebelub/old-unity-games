using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour {

    public float startHealth = 1f;

    public float health = 1f;

    private SpriteRenderer sr;

    private bool isPlayer;

    private Player player;

    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;

        sr = GetComponentInChildren<SpriteRenderer>();

        player = gameObject.GetComponent<Player>();

        if (player != null)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        AddHealth(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle == null)
        {
            if (isPlayer)
            {
                Oxygen oxygen = collision.gameObject.GetComponent<Oxygen>();

                if (oxygen != null)
                {
                    health += oxygen.health;
                    if (health > startHealth)
                        health = startHealth;

                    UpdateColor();

                    // Do so that the oxygen looses health, will just kill it for now ._.
                    oxygen.Eliminate();
                }
            }

            return;
        }

        if (health <= obstacle.damage)
        {
            obstacle.damage -= health;
            obstacle.UpdateStuff();
            health = 0;
            UpdateColor();
            if (isPlayer)
            {
                Die();
            }
            else
            {
                Eliminate();
            }
        }
        else
        {
            AddHealth(-obstacle.damage);
            UpdateColor();
            obstacle.damage = 0;
            obstacle.UpdateStuff();

        }
    }

    public void AddHealth(float ammount)
    {
        health += ammount;

        if (isPlayer)
        {
            player.ResizeToSize(health);
        }
    }

    public void UpdateColor()
    {
        float blueness = health / startHealth;

        sr.color = new Color(128f / 255, 149f / 255, blueness);
    }

    public void Die()
    {
        gm.gameOver.SetActive(true);
        gm.isGameOver = true;
    }

    public void Eliminate()
    {
        Destroy(gameObject);
    }

    public void Respawn()
    {
        // Healing player
        health = startHealth;

        UpdateColor();


        if (gm.isGameOver == true)
        {
            StartCoroutine(gm.ISpawnObstacles());
            gm.isGameOver = false;

        }

    }

}
