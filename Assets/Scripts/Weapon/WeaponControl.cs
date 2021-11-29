using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class WeaponControl : MonoBehaviour
{
    public int indexGun = -1;
    [SerializeField] Transform gunContain;
    Dictionary<int, WeaponBehaviour> arrWeapons = new Dictionary<int, WeaponBehaviour>();
    WeaponBehaviour currentWeapon;

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
        currentWeapon = arrWeapons.ElementAt(0).Value;
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
}
