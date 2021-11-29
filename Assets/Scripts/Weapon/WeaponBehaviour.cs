using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWeaponHandle
{
    void FireHandle(Vector3 rayOrigin);
    void SetupWeapon(WeaponBehaviour weapon);
}

public interface IWeaponReload
{
    void ReloadHandle();
}

public class WeaponBehaviour : MonoBehaviour
{
    public IWeaponHandle iWeaponHandle;
    public IWeaponReload iWeaponReload;
    
    public float accuracy = 80.0f;
    public float currentAccuracy;
    public float accuracyDropPerShot = 1.0f;
    public float accuracyRecoverRate = 0.1f;
    public float accuracyViewportDrop = 0.1f;
    public float crosshairCurentDrop = 0.0f;
    public float crosshairDropPerShot = 5.0f;
    public float crosshairRecoverRate = 3.0f;

    public Transform prefabImpactEnemy;
    public Transform prefabImpactConcrete;
    public AudioSource audioSource;
    public AudioClip[] fireSound;
    public AudioClip reloadSound;
    public AudioClip readySFX;
    public AudioClip drySFX;

    public bool muzzleFlash = false;
    public GameObject muzzle;

    GunDataBase dataBase;
    WeaponControl weaponControl;

    public virtual void OnSetupGun(GunDataBase dataBase)
    {
        this.dataBase = dataBase;
    }

    public virtual void OnChangeGun()
    {

    }

    public virtual void Update() { }

}
