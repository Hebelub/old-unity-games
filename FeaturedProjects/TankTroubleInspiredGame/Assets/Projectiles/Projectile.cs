using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tank tank;
    public Rigidbody2D rb;
    public float shootVelocity = 5.0F;
    public float shootDelay = 1.0F;
    public float lifeTime = 5.0F;

    public Barrel barrel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        rb.velocity = tank.aimDirection.normalized * shootVelocity;
        StartCoroutine(IDestroyAfterTime(lifeTime));
    }

    public IEnumerator IDestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Kill();
    }

    private void Update()
    {
        rb.velocity = rb.velocity.normalized * shootVelocity;
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

}
