using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    #region Singelton
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }
    #endregion

    public event Action OnTankChangeWeapon;
    public void TankChangeWeapon()
    {
        OnTankChangeWeapon?.Invoke();
    }

    public event Action OnTankShoot;
    public void TankShoot()
    {
        OnTankShoot?.Invoke();
    }

    public event Action OnTankReadyToShoot;
    public void TankReadyToShoot()
    {
        OnTankReadyToShoot?.Invoke();
    }

}
