using System.Collections;
using UnityEngine;
using Entitas;

public class CreateEnemySystem : IInitializeSystem
{
    readonly GameContext _gameContext;
    readonly SettingsContext _settingsContext;

    public CreateEnemySystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _settingsContext = contexts.settings;
    }

    public void Initialize()
    {
        for (int i = 0; i < MissionControl.PathEnemy.Count; i++)
        {
            GameObject goZombie = Object.Instantiate(_settingsContext.gameSettings.value.enemySettings.enemyPrefab);
            EnemyControl enemy = goZombie.GetComponent<EnemyControl>();
            EnemyData enemyData = new EnemyData() { path = _gameContext.spawnEnemyPoint.spawnPointEnemy[i] };
            enemy.OnSetup(enemyData);
            SpawCharacterControl.enemyList.Add(enemy);
        }
        //var gameObject = Object.Instantiate(_settingsContext.gameSettings.value.playerSettings.playerPrefab);
        //gameObject.transform.position = _gameContext.spawnPoint.position;
        //gameObject.transform.rotation = _gameContext.spawnPoint.rotation;
    }
}
