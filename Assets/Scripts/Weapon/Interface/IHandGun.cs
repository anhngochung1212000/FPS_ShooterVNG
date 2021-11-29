using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHandGun : IWeaponHandle, IWeaponReload
{
    private HandGunControl weapon;
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
        this.weapon = (HandGunControl)weapon;
    }
}
