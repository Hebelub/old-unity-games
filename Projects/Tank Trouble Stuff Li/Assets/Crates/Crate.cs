using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Ammo[] reward;

    public Projectile[] projectiles = new Projectile[1];
    public int[] quantities = new int[1];

    private void Start()
    {
        reward = new Ammo[projectiles.Length];

        int i = 0;
        foreach(Projectile projectile in projectiles)
        {
            if (quantities.Length <= i)
            {
                Debug.LogError("Projectile[] projectiles and int[] quantities should be the same Lenght");
                reward[i] = new Ammo(0, projectile);   
                continue;
            }
            reward[i] = new Ammo(quantities[i], projectile);
            i++;
        }
    }

    public void OpenCrate(Tank opener)
    {
        foreach (Ammo ammo in reward)
        {
            opener.storage.AddAmmo(ammo);
        }

        EmptyCrate();

        Destroy(gameObject);
    }

    public void EmptyCrate()
    {
        reward = new Ammo[0];
    }

}
