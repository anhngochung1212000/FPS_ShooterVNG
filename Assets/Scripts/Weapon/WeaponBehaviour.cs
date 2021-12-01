using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public float rof;
    public float accuracy = 80.0f;
    public float accuracyDropPerShot = 1.0f;
    public float accuracyRecoverRate = 0.1f;
    public float accuracyViewportDrop = 0.1f;

    public float weaponRange=10;
    Camera fpsCam;
    [SerializeField] Transform prefabImpactEnemy;
    [SerializeField] Transform prefabImpactConcrete;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] fireSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip readySFX;
    [SerializeField] AudioClip drySFX;
    [SerializeField] Transform gunAim;
    [SerializeField] bool muzzleFlash = false;
    [SerializeField] GameObject muzzle;
    public CrossHairControl crossHair;
    public bool tracer = false;
    public Transform tracerPrefab;
    public Transform tracerTransform;
    public float tracerSpeed = 200;
    public GunDataBase dataBase;
    public bool isReloading;
    public bool isFire;
    public float timeFire;
    public int countBulletCurent;
    WeaponControl weaponControl;
    [NonSerialized]
    public SpawnPool poolBullet;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask enemyMask;

    public virtual void OnSetupGun(GunDataBase dataBase)
    {
        this.dataBase = dataBase;
        fpsCam = GetComponentInParent<Camera>();
        rof = 60 / dataBase.rateOfFire;
        accuracy = dataBase.accuracy;
        poolBullet = PoolManager.Pools["PoolBulletPlayer"];
        countBulletCurent = dataBase.clipSizeBullet;
        CreatePrefabPool(prefabImpactEnemy);
        CreatePrefabPool(tracerPrefab);
        CreatePrefabPool(prefabImpactConcrete);
    }

    public virtual void OnChangeGun()
    {

    }

    void OnEnable()
    {
        audioSource.PlayOneShot(readySFX);
    }

    public virtual void Update() {
        timeFire += Time.deltaTime;

        if(PlayerControl.isAutoFire && !PlayerControl.isDead && !MissionControl.isVictory)
            DetectEnemyAuto();
    }

    public void HideGun()
    {
        gameObject.SetActive(false);
    }

    public void DetectEnemyAuto()
    {
        // Create a vector at the center of our camera's viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 aim = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, weaponRange));
        //Vector3 aimRandom = fpsCam.ViewportToWorldPoint(new Vector3(0.5f +x, 0.5f +y, weaponRange));
        Vector3 dir = aim - rayOrigin;
        //Vector3 dirRandom = aimRandom - rayOrigin;
        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;
        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, dir.normalized, out hit, weaponRange, enemyMask))
        {
            crossHair.notTarget.SetActive(false);
            Debug.DrawRay(gunAim.position, dir, Color.red);
            crossHair.SetColorCrossHair(Color.red);
            if (isReloading)
                return;
            isFire = true;
            FireGun(rayOrigin);

        }
        else
        {
            crossHair.SetColorCrossHair(Color.white);
            isFire = false;
            crossHair.isFire = false;
            if (Physics.Raycast(rayOrigin, dir.normalized, out hit, weaponRange * 10, enemyMask))
            {
                crossHair.notTarget.SetActive(true);
            }
            else
                crossHair.notTarget.SetActive(false);
            Debug.DrawRay(gunAim.position, dir * weaponRange, Color.green);
            Debug.DrawRay(rayOrigin, dir * weaponRange, Color.blue);
        }

    }

    void FireGun(Vector3 rayOrigin)
    {
        if (timeFire <= rof || isReloading)
            return;

        if (countBulletCurent <= 0)
            return;

        timeFire = 0;

        FireEffects();

        countBulletCurent--;

        WeaponControl.UpdateBulletAmount(this);

        //Fire Action
        float accuracyVary = (100 - accuracy) / 100 * accuracyViewportDrop;
        accuracyVary *= 100;
        float x = UnityEngine.Random.Range(-0.01f, 0.01f) * accuracyVary / 1.5f;
        float y = UnityEngine.Random.Range(-0.01f, 0.01f) * accuracyVary / 1.5f;

        Vector3 aimRandom = fpsCam.ViewportToWorldPoint(new Vector3(0.5f + x, 0.5f + y, weaponRange));
        Vector3 dirRandom = aimRandom - rayOrigin;

        RaycastHit hitEnemy;

        if (!Physics.Raycast(rayOrigin, dirRandom.normalized, out hitEnemy, weaponRange * 10))
            return;

        Tracer(dirRandom.normalized, 2f);
        Transform tr = null;
        if (hitEnemy.collider.tag == "Enemy")
        {
            EnemyControl enemy = hitEnemy.collider.GetComponent<EnemyControl>();
            if (enemy != null)
                enemy.OnHit(dataBase.damage);
            tr = poolBullet.Spawn(prefabImpactEnemy.name);
        }
        else if (hitEnemy.collider.tag == "Ground")
        {
            tr = poolBullet.Spawn(prefabImpactConcrete.name);
        }

        if (tr != null)
        {
            tr.SetParent(null);
            tr.position = hitEnemy.point;
            tr.rotation = Quaternion.FromToRotation(Vector3.up, hitEnemy.normal);
            tr.GetComponent<BulletImpact>().Settup("PoolBulletPlayer");
        }

        if (countBulletCurent > 0)
            return;

        ReLoadBullet();
    }

    public void ReLoadBullet()
    {
        StartCoroutine(ReLoadBulletCoroutine());
    }

    void FireEffects()
    {
        StartCoroutine(PlayMuzzle());
        audioSource.PlayOneShot(fireSound[UnityEngine.Random.Range(0, fireSound.Length)]);
        animator.SetTrigger("Fire");
        crossHair.SetCurrentSize();
    }

    IEnumerator PlayMuzzle()
    {
        if (!muzzleFlash)
            yield break;

        muzzle.SetActive(true);
        muzzle.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
        yield return new WaitForSeconds(0.03f);
        muzzle.SetActive(false);
    }

    void CreatePrefabPool(Transform prefabPoolNeed)
    {
        if (PoolManager.Pools["PoolBulletPlayer"].prefabPools.ContainsKey(prefabPoolNeed.name))
            return;

        PrefabPool prefabPool = new PrefabPool(prefabPoolNeed);
        prefabPool.preloadAmount = (int)(dataBase.clipSizeBullet * 0.5f);
        prefabPool.limitAmount = (int)(dataBase.clipSizeBullet * 0.5f);
        prefabPool.limitInstances = true;
        prefabPool.limitFIFO = true;
        prefabPool.cullDespawned = false;
        PoolManager.Pools["PoolBulletPlayer"].CreatePrefabPool(prefabPool);
    }

    IEnumerator ReLoadBulletCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(drySFX);
        yield return new WaitForSeconds(0.3f);
        
        audioSource.PlayOneShot(reloadSound);
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(dataBase.replaceBulletTime);
        countBulletCurent = dataBase.clipSizeBullet;
        crossHair.SetColorCrossHair(Color.white);
        WeaponControl.UpdateBulletAmount(this);
        isFire = false;
        isReloading = false;
        crossHair.isFire = false;
    }


    void Tracer(Vector3 direction, float tracerLifeTime)
    {
        if (!tracer)
            return;

        Transform t = PoolManager.Pools["PoolBulletPlayer"].Spawn(tracerPrefab.name);
        t.transform.SetParent(null);
        t.transform.position = tracerTransform.position;
        t.transform.rotation = tracerTransform.rotation;
        t.GetComponent<Rigidbody>().velocity = direction * tracerSpeed;
        PoolManager.Pools["PoolBulletPlayer"].Despawn(t, tracerLifeTime);
    }
}
