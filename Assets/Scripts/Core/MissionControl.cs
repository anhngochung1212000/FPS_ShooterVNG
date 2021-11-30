using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField] List<PathManager> pathEnemy = new List<PathManager>();
    public static Action onPlayerVictory;
    IEnumerator Start()
    {
        for (int i = 0; i < pathEnemy.Count; i++)
        {
            GameObject goZombie = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy01")) as GameObject;
            EnemyControl enemy = goZombie.GetComponent<EnemyControl>();
            EnemyData enemyData = new EnemyData() { path = pathEnemy[i] };
            enemy.OnSetup(enemyData);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 2f));
        }
    }
}
