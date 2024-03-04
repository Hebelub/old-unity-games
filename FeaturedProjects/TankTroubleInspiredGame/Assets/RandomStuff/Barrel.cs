using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    public Tank tank;

    public Transform barrelEnd;
    public Transform shape;
    public float recoilMagnitude = 0.05F;

    public bool readyForNextShot = true;

    public new ParticleSystem particleSystem;

    public float ammo = 0; // How much ammo is left?
    public Projectile projectile; // Which projectile does the barrel use
    public void Shoot() // Shooting has to be moved from Tank an in barrel
    {
        tank.events.Shoot();

        GameObject newProjectile = Instantiate(projectile.gameObject, tank.barrel.barrelEnd.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().tank = tank;

        // tank.barrel.AnimateShot();
    }

    private void Awake()
    {
        particleSystem = barrelEnd.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        if(tank != null)
        {
            tank.events.OnShoot += AnimateShot;
        }
    }

    private void OnDestroy()
    {
        if (tank != null)
        {
            tank.events.OnShoot -= AnimateShot;
        }
    }
    
    public void ChangeToThisBarrel()
    {
        Destroy(tank.barrel);
        AttachBarrel(tank);
    }

    public void AttachBarrel(Tank attachTo)
    {
        tank = attachTo;
        Instantiate(gameObject, attachTo.transform.position, Quaternion.identity, attachTo.transform);
        Extend();
    }

    public virtual void Extend()
    {
        StartCoroutine(IExtendBarrel(20F));

        IEnumerator IExtendBarrel(float speed)
        {
            transform.localScale = new Vector3(0, 1, 1);

            while (transform.localScale.x <= 1)
            {
                transform.localScale += Vector3.right * speed * Time.deltaTime;
                
                yield return null;
            }
            transform.localScale = Vector3.one;
        }
    }
    public virtual void Retract(int change)
    {
        StartCoroutine(IRetractBarrel(20F));

        IEnumerator IRetractBarrel(float speed)
        {
            transform.localScale = Vector3.one;

            while(transform.localScale.x >= 0)
            {
                transform.localScale -= Vector3.right * speed * Time.deltaTime;

                yield return null;
            }
            transform.localScale = new Vector3(0, 1, 1);

            tank.storage.ChangeToRelativeWeapon(change);

            Destroy(gameObject);
        }
    }

    public virtual void AnimateShot()
    {
        NormalAnimation();
    }

    public void NormalAnimation()
    {
        StartCoroutine(IRecoil(recoilMagnitude));
        ParticlesOnShot();

    }

    public virtual void ParticlesOnShot()
    {
        particleSystem.Play();
    }

    public virtual IEnumerator IRecoil(float magnitude)
    {
        readyForNextShot = false;

        for (int i = 0; i < 4; i++)
        {
            transform.position -= transform.right * magnitude;
            yield return new WaitForSeconds(0.005f);
        }

        yield return new WaitForSeconds(0.1F);

        for (int i = 0; i < 16; i++)
        {
            transform.position += transform.right * (magnitude / 4);
            yield return new WaitForSeconds(0.005f);
        }

        readyForNextShot = true;
    }

    public virtual void SetColor(Color color)
    {
        SpriteRenderer[] srs = shape.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sr in srs)
        {
            sr.color = color;
        }
    }
}
