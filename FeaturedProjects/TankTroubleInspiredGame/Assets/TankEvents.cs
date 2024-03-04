 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class TankEvents : MonoBehaviour
{
    public TankEvents()
    {

    }

    public event Action OnChangeWeapon;
    public void ChangeWeapon()
    {
        OnChangeWeapon?.Invoke();
    }

    public event Action OnShoot;
    public void Shoot()
    {
        OnShoot?.Invoke();
    }

    public event Action OnPickCrate;
    public void OpenCrate()
    {
        OnPickCrate?.Invoke();
    }

    public event Action OnTakeDamage;
    public void TakeDamage()
    {
        OnTakeDamage?.Invoke();
    }

}
