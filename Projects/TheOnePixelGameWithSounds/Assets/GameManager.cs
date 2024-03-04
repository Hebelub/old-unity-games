using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public KeyCode attack = KeyCode.Space;

    public Monster[] monsters;

    public AudioSource aS;

    #region The Three Collours
    public float attackDamage = 1f;

    private float health = 1f; // Red
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health < 0) health = 0;
            if (health > 1) health = 1;
            RefreshColor();
        }
    }

    private float precense = 0f; // Green
    public float Precense
    {
        get
        {
            return precense;
        }
        set
        {
            precense = value;
            if (precense < 0) precense = 0;
            if (precense > 1) precense = 1;
            RefreshColor();
        }
    }

    private float power = 0f; // Blue
    public float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
            if (power < 0) power = 0;
            if (power > 1) power = 1;
            RefreshColor();
        }
    }
    #endregion

    public float chargeSpeed = 0.1f;
    public float powerForAttack = 0.25f;

    public float timeUntillNextMonster;

    public void RefreshColor()
    {
        cam.backgroundColor = new Color(
            Health,           // Red 
            1f - Precense,    // Green
            Power);           // Blue
    }

    void Start()
    {
        cam = Camera.main;

        timeUntillNextMonster = Random.Range(1, 6);

        currentMonster = GetRandomMonster();
    }

    void Update()
    {
        // Health
        if (Power >= 1)
        {
            Health += chargeSpeed * Time.deltaTime;
        }

        // Power
        if (Power < 1)
        {
            Power += chargeSpeed * Time.deltaTime;
        }
        else if (Power > 1)
        {
            Power = 1;
        }

        // Attack
        if (Input.GetKeyDown(attack))
        {
            if (power >= powerForAttack)
            {
                power -= powerForAttack;
                currentMonster.h -= attackDamage * Precense;
                StartCoroutine(Knockback(false)); // Precense -= currentMonster.damadgedKnockBack;
                aS.PlayOneShot(currentMonster.damadgedSound);
            }
            else
            {
                // Not enough power sound
            }
        }

        // Monster
        timeUntillNextMonster -= Time.deltaTime;
        if (timeUntillNextMonster <= 0)
        {
            if (!isMonsterSpawned)
            {
                aS.PlayOneShot(currentMonster.spawnSound);
                aS.clip = currentMonster.stepSound;
                aS.Play();
            }
            isMonsterSpawned = true;

            AttackingMonster();
            
        }

        if (Health <= 0)
        {
            aS.Stop();
            aS.PlayOneShot(currentMonster.happySound);
            currentMonster = GetRandomMonster();
            timeUntillNextMonster = Random.Range(2, 6);
            Precense = 0;
            Health = 1f;
            Power = 1f;
        }

    }
    bool isMonsterSpawned = false;
    bool monsterIsKnockingBack = false;

    public void AttackingMonster()
    {
        if (!monsterIsKnockingBack)
        {
            Precense += currentMonster.moveSpeed * Time.deltaTime;
        }

        if (currentMonster.h <= 0)
        {
            MonsterDefeated();
        }
        else if (Precense >= 1f - currentMonster.attackRange)
        {
            Health -= currentMonster.hitDamage;
            StartCoroutine(Knockback(true)); // Precense -= currentMonster.attackKnockBack;
            aS.PlayOneShot(currentMonster.attackingSound);
        }


    }
    public void MonsterDefeated()
    {
        aS.Stop();
        aS.PlayOneShot(currentMonster.deathSound);
        currentMonster = GetRandomMonster();
        timeUntillNextMonster = Random.Range(2, 7);
        Precense = 0;
        isMonsterSpawned = false;
    }

    public Monster GetRandomMonster ()
    {

        Monster m = monsters[Random.Range(0, monsters.Length - 1)];
        m.h = m.health;
        return m; 
    }

    public IEnumerator Knockback(bool isAttacking)
    {

        float knockbackPower;
        float knockbackTime;

        if (isAttacking)
        {
            knockbackPower = currentMonster.attackKnockBack;
            knockbackTime = currentMonster.hitKnockbackTime;
        }
        else
        {
            knockbackPower = currentMonster.damadgedKnockBack;
            knockbackTime = currentMonster.attackedKnockbackTime;
        }

        while (knockbackTime > 0)
        {
            monsterIsKnockingBack = true;

            Precense -= knockbackPower * Time.deltaTime;

            knockbackTime -= Time.deltaTime;

            yield return null;
        }
        monsterIsKnockingBack = false;
    }

    public Monster currentMonster;

}

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster", order = 1)]
public class Monster : ScriptableObject
{
    public AudioClip spawnSound;
    public AudioClip stepSound;
    public AudioClip damadgedSound;
    public AudioClip attackingSound;
    public AudioClip deathSound;
    public AudioClip happySound;

    public float moveSpeed;
    public float health;
    public float hitDamage;
    public float attackRange;
    public float attackKnockBack;
    public float damadgedKnockBack;
    public float attackedKnockbackTime;
    public float hitKnockbackTime;

    public float h;
}
