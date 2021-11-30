using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WeaponControl : MonoBehaviour
{
    static int indexGun = -1;
    [SerializeField] Transform gunContain;
    static Dictionary<int, WeaponBehaviour> arrWeapons = new Dictionary<int, WeaponBehaviour>();
    public static WeaponBehaviour currentWeapon;
    public static event Action<WeaponBehaviour> onChangeWeapon;
    public static event Action<WeaponBehaviour> onUpdateBulletAmount;

    void Start()
    {
        ConfigManager.InitGameData();

        foreach (var configWeapon in ConfigManager.gunData)
        {
            InstantiateGun(configWeapon.Value);
        }

        if (arrWeapons.Count == 0)
            return;

        indexGun = 0;
        currentWeapon = arrWeapons.ElementAt(indexGun).Value;
        currentWeapon.gameObject.SetActive(true);
    }

    void InstantiateGun(GunDataBase configWeapon)
    {
        GameObject gun = Instantiate(Resources.Load("Prefabs/" + configWeapon.prefabName, typeof(GameObject))) as GameObject;
        gun.transform.SetParent(gunContain);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localScale = Vector3.one;
        WeaponBehaviour weaponAbstract = gun.GetComponent<WeaponBehaviour>();
        weaponAbstract.OnSetupGun(configWeapon);
        arrWeapons.Add(configWeapon.id,weaponAbstract);
        gun.SetActive(false);
       
    }

    public static void UpdateBulletAmount(WeaponBehaviour weaponBehaviour)
    {
        onUpdateBulletAmount?.Invoke(weaponBehaviour);
    }

    public static void ChangeGun()
    {
        indexGun++;
        if (indexGun > arrWeapons.Count - 1)
            indexGun = 0;

        if (currentWeapon != null)
            currentWeapon.HideGun();

        currentWeapon = arrWeapons.ElementAt(indexGun).Value;
        currentWeapon.gameObject.SetActive(true);

        onChangeWeapon?.Invoke(currentWeapon);
    }
}
