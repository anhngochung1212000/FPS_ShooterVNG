using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRifleGun : IWeaponHandle, IWeaponReload
{
    private RifleGunControl weapon;
    public void FireHandle(Vector3 rayOrigin)
    {
        //weapon.FireMuzzle();
        //weapon.GunFire(rayOrigin);
    }

    public void ReloadHandle()
    {
        //weapon.StartReload();
    }

    public void SetupWeapon(WeaponBehaviour weapon)
    {
        this.weapon = (RifleGunControl)weapon;
    }
}
