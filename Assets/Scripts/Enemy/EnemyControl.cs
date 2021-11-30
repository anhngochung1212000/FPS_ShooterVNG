using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : FSMSystem
{
    public int damge;
    public int totalHeal;
    public int currentHeal;
    public event Action<int, int> onHpChange;
    public navMove moveNavi;
    public FieldOfView fieldOfView;
    [HideInInspector] public Transform trans;
    [HideInInspector] public Action callbackStopFindVisiblePlayer = null;
    [HideInInspector] public bool isWalk;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isStop;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public float timerCountRof;
    public Material materialDissovle;
    Renderer[] arrRendererMesh;

    public float rof = 1;
    public float rangeAttack = 3;

    public int CurrentHeal
    {
        get
        {
            return currentHeal;
        }
        set
        {
            currentHeal = value;
            onHpChange?.Invoke(value, totalHeal);
        }
    }

    void Awake()
    {
        trans = transform;
    }

    public virtual void OnSetup(object data)
    {
        EnemyData enemyData = (EnemyData)data;
        moveNavi.pathContainer = enemyData.path;
        currentHeal = totalHeal;

    }

    public virtual void OnHit(int damage)
    {

    }
    public override void SystemUpdate()
    {
        base.SystemUpdate();
    }

    IEnumerator SetDisovleEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        arrRendererMesh = GetComponentsInChildren<Renderer>();
        Material newMaterial = UnityEngine.Object.Instantiate(materialDissovle);
        for (int i = 0; i < arrRendererMesh.Length; i++)
        {
            if (arrRendererMesh[i].gameObject.tag == "DissolveMesh")
            {
                arrRendererMesh[i].material = newMaterial;
                arrRendererMesh[i].material.SetFloat("_DissolveWidth", 0.07f);
                StartCoroutine("WaitChangeDissolveAmount");
            }

        }
    }
    IEnumerator WaitChangeDissolveAmount()
    {
        yield return new WaitForSeconds(0.1f);
        float amount = 0;
        while (amount < 1)
        {
            amount += Time.deltaTime *2;

            for (int i = 0; i < arrRendererMesh.Length; i++)
            {
                arrRendererMesh[i].material.SetFloat("_DissolveAmount", amount);
            }
            yield return null;
        }
        //MiniMapControl.instance.RemoveItem(indexInMiniMap.ToString());
        //EnemyDie?.Invoke();
        Destroy(gameObject);
    }

    IEnumerator StartFOV()
    {
        yield return new WaitForSeconds(1f);
        fieldOfView.viewMeshFilter.gameObject.SetActive(true);
    }

    public void LookPlayer()
    {
        Vector3 target = new Vector3(PlayerControl.instance.trans.position.x, trans.position.y, PlayerControl.instance.trans.transform.position.z);
        Vector3 dirEnemy = target - trans.position;
        Quaternion quaternionLook = Quaternion.LookRotation(dirEnemy, Vector3.up);
        trans.localRotation = quaternionLook; //Quaternion.RotateTowards(trans.localRotation, quaternionLook, Time.deltaTime * 360);// 
    }
}
