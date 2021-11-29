using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGunControl : WeaponBehaviour
{
    public override void OnSetupGun(GunDataBase dataBase)
    {
        IRifleGun iwp = new IRifleGun();
        iWeaponHandle = iwp;
        iWeaponReload = iwp;
        iWeaponHandle.SetupWeapon(this);

        base.OnSetupGun(dataBase);
    }
}
