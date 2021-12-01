using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField] List<PathManager> pathEnemy = new List<PathManager>();
    static MissionControl Instance;
    void Awake()
    {
        Instance = this;
    }

    public static List<PathManager> PathEnemy { get { return Instance.pathEnemy; } }
    public static int EnemyDead { get { return Instance.enemyDead; } }
    public static Action onPlayerVictory;
    public static Action<int,int> onEnemyDead;
    static int countEnemy = 0;
    public static bool isVictory;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => onEnemyDead != null);
        onEnemyDead.Invoke(countEnemy, enemyDead);
    }

    public static int CountEnemy
    {
        get
        { return countEnemy; }
        set
        {
            countEnemy = value;
            onEnemyDead?.Invoke(countEnemy, EnemyDead);
            if (countEnemy >= EnemyDead)
            {
                isVictory = true;
                onPlayerVictory?.Invoke();
            }
                
        }
    }

    public int enemyDead = 10;


}
