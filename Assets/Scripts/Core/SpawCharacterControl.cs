using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Entitas;
public class SpawCharacterControl : MonoBehaviour
{
    [SerializeField] Transform posPlayer;
    static SpawCharacterControl Instance;
    public static List<EnemyControl> enemyList = new List<EnemyControl>();

    public static Transform PosPlayer { get { return Instance.posPlayer; } }

    void Awake()
    {
        Instance = this;
        GameManager.onReloadGameData += OnReloadGameData;

    }
    void OnDestroy()
    {
        GameManager.onReloadGameData -= OnReloadGameData;
    }


    public static void SpawEnemy()
    {
        enemyList.Clear();
        for (int i = 0; i < MissionControl.PathEnemy.Count; i++)
        {
            GameObject goZombie = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy01")) as GameObject;
            EnemyControl enemy = goZombie.GetComponent<EnemyControl>();
            EnemyData enemyData = new EnemyData() { path = MissionControl.PathEnemy[i] };
            enemy.OnSetup(enemyData);
            enemyList.Add(enemy);
        }
    }

    void OnReloadGameData()
    {
        for (int i = enemyList.Count - 1; i >=0; i--)
        {
            enemyList[i].EnemyOnDead(false);
        }

        SpawEnemy();
    }
}
