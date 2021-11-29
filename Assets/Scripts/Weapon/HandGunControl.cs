using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunControl : WeaponBehaviour
{
    public override void OnSetupGun(GunDataBase dataBase)
    {
        IHandGun iwp = new IHandGun();
        iWeaponHandle = iwp;
        iWeaponReload = iwp;
        iWeaponHandle.SetupWeapon(this);

        base.OnSetupGun(dataBase);
    }
}
