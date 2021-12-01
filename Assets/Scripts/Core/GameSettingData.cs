using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class GameSettingData : ScriptableObject
{
    [Serializable]
    public class PlayerSettings
    {
        public GameObject playerPrefab;
    }

    [Serializable]
    public class EnemySettings
    {
        public GameObject enemyPrefab;
    }

    [Header("Player Settings")]
    public PlayerSettings playerSettings;

    [Header("Player Settings")]
    public EnemySettings enemySettings;
}
